using BFES.DataAccess;
using BFES.DataAccess.Common;
using BFES.Models;
using BKYL.BFES.Plugin.Models;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BKYL.BFES.Plugin
{
    class XC1500_ConvertTuyAndAnalysiData : BaseJob
    {
        public struct MaterialCodeInfo
        {
            public int Type;
            public int Min_Code;
            public int Max_Code;
            public List<int> L_ExtraCode;
        }

        List<MaterialCodeInfo> L_MaterialCodeInfo = new List<MaterialCodeInfo>();
        List<L3MATERIAL_CONFIG> L_L3MATERIAL_CONFIG = new List<L3MATERIAL_CONFIG>();
        List<L3MATERIAL> L_L3MATERIAL = new List<L3MATERIAL>();
        Dictionary<int, MaterialCodeInfo> dicCodeType = new Dictionary<int, MaterialCodeInfo>();
        public override void InitTask()
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                try
                {
                    L_L3MATERIAL_CONFIG = idatabase.GetList<L3MATERIAL_CONFIG>("select * from L3MATERIAL_CONFIG");

                    foreach (L3MATERIAL_CONFIG l3MaterialCnfg in L_L3MATERIAL_CONFIG)
                    {
                        MaterialCodeInfo materialCodeInfo = new MaterialCodeInfo();
                        materialCodeInfo.Type = l3MaterialCnfg.M_TYPE;
                        materialCodeInfo.Min_Code = l3MaterialCnfg.CODE_MIN;
                        materialCodeInfo.Max_Code = l3MaterialCnfg.CODE_MAX;
                        materialCodeInfo.L_ExtraCode = new List<int>();
                        if (l3MaterialCnfg.CODE_EXTRA != null)
                        {
                            foreach (string OneStrCode in l3MaterialCnfg.CODE_EXTRA.Split(','))
                            {
                                materialCodeInfo.L_ExtraCode.Add(OneStrCode.ToInt());
                            }
                        }
                        L_MaterialCodeInfo.Add(materialCodeInfo);
                    }
                    dicCodeType.Add(0, L_MaterialCodeInfo.Find(m => m.Type == 2));
                    dicCodeType.Add(1, L_MaterialCodeInfo.Find(m => m.Type == 6));
                    dicCodeType.Add(2, L_MaterialCodeInfo.Find(m => m.Type == 7));
                    dicCodeType.Add(3, L_MaterialCodeInfo.Find(m => m.Type == 8));
                    dicCodeType.Add(4, L_MaterialCodeInfo.Find(m => m.Type == 4));
                    dicCodeType.Add(5, L_MaterialCodeInfo.Find(m => m.Type == 5));
                    dicCodeType.Add(6, L_MaterialCodeInfo.Find(m => m.Type == 3));

                }
                catch (Exception)
                {
                    throw;
                }
            }
            return;
        }

        public override void RunTask(DateTime currentTime)
        {
            ConvertTuyData(currentTime);
            ConVertL3MaterialData(currentTime);
        }

        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            //输入错误到界面
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            //输出错误到日志
            Writelog(exception.Message + exception.StackTrace);
        }

        public void ConvertTuyData(DateTime currentTime)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                try
                {
                    DataTable dt = idatabase.GetDataTable("select TUYERE_ID,DIAMETER,TURYERE_LENGTH,CHANGE_DATA from MES_PUBLIC_TUYERE_2");
                    idatabase.BeginTran();
                    foreach (DataRow OneRow in dt.Rows)
                    {
                        int TuyNo = OneRow["TUYERE_ID"].ToInt();
                        double Diameter = OneRow["DIAMETER"].ToDouble();
                        double Length = OneRow["TURYERE_LENGTH"].ToDouble();
                        DateTime time = OneRow["CHANGE_DATA"].ToDate();
                        string strSql = $"MERGE INTO TY_TYSTATUS T1 USING (SELECT {UtilsConvert.GetOraString(time)} AS TIMESTAMP," +
                            $"{TuyNo} AS TUYERESNO,{Diameter} AS DIAMETER,{Length} AS LENGTH FROM dual) T2 ON(T1.TUYERESNO = T2.TUYERESNO) " +
                            $"WHEN MATCHED THEN UPDATE SET T1.TIMESTAMP = T2.TIMESTAMP,T1.DIAMETER = T2.DIAMETER,T1.LENGTH = T2.LENGTH " +
                            $"WHEN NOT MATCHED THEN INSERT (TIMESTAMP,TUYERESNO,DIAMETER,LENGTH) VALUES (T2.TIMESTAMP,T2.TUYERESNO,T2.DIAMETER,T2.LENGTH)";
                        idatabase.ExecuteCommand(strSql);
                    }
                    idatabase.CommitTran();
                    Outputlog(string.Format("{0}条风口信息记录已更新！", dt.Rows.Count));
                }
                catch (Exception)
                {
                    idatabase.RollbackTran();
                    throw;
                }
            }
        }

        public void ConVertL3MaterialData(DateTime currentTime)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                try
                {
                    DataTable dt_S = idatabase.GetDataTable("select MAT_CODE,E_NAME,C_NAME,C_TYPE,DENSITY,MAT_DESC,MES_CODE from MES_PUBLIC_MATERIAL_2 WHERE C_TYPE<7");
                    L_L3MATERIAL.Clear();
                    L_L3MATERIAL = idatabase.GetList<L3MATERIAL>("select TIMESTAMP,FLAG,CODE,CODEL3,NAME,DENSITY,MOISTURE,CODEL1,PERVALUE,M_TYPE from L3MATERIAL");
                    List<L3MATERIAL> L_L3MATERIAL_Ins = new List<L3MATERIAL>();
                    foreach (DataRow oneRow in dt_S.Rows)
                    {
                        int Code_s = oneRow["MAT_CODE"].ToInt();
                        // if (!L_Code.Contains(Code_s))
                        if (L_L3MATERIAL.Find(m=>m.CODEL1.ToInt() == Code_s) == null)
                        {
                            L3MATERIAL l3Material = new L3MATERIAL();
                            MaterialCodeInfo materialCodeInfo = dicCodeType[oneRow["C_TYPE"].ToInt()];
                            l3Material.TIMESTAMP = currentTime;
                            l3Material.FLAG = true;
                            l3Material.CODE = -1;
                            for (int i = materialCodeInfo.Min_Code; i <= materialCodeInfo.Max_Code; i++)
                            {
                                if (materialCodeInfo.L_ExtraCode.Contains(i) || L_L3MATERIAL.Find(m=>m.CODE == i) != null)
                                {
                                    continue;
                                }
                                l3Material.CODE = i;
                                break;
                            }
                          
                            l3Material.CODEL3 = oneRow["MES_CODE"].ToString();
                            l3Material.NAME = oneRow["C_NAME"].ToString();
                            l3Material.DENSITY = oneRow["DENSITY"].ToDouble()/1000;
                            l3Material.MOISTURE = 0;
                            l3Material.CODEL1 = oneRow["MAT_CODE"].ToString();
                            l3Material.PERVALUE = 0;
                            l3Material.M_TYPE = materialCodeInfo.Type;
                            if (l3Material.CODE == -1)
                            {
                                Outputlog(string.Format("物料{0}未获取到索引！", l3Material.NAME));
                                Log.WriteLine(string.Format("物料{0}未获取到索引！", l3Material.NAME), GlobalObject.RunFolderName);
                            }
                            else
                            {
                                L_L3MATERIAL_Ins.Add(l3Material);
                                L_L3MATERIAL.Add(l3Material);
                                string sql = $"insert into L3MATERIAL (timestamp,Flag,code,codel3,name,density,moisture,codel1,pervalue,m_type) values " +
                                          $"  ({DataBaseFactory.ConvertTimeString(l3Material.TIMESTAMP, S_DBSource.DBType)},0,{l3Material.CODE},'{l3Material.CODEL3}','{l3Material.NAME}',{l3Material.DENSITY},{l3Material.MOISTURE},'{l3Material.CODEL1}',{l3Material.PERVALUE},{l3Material.M_TYPE})";

                                idatabase.ExecuteCommand(sql);
                            }
                            
                        }

                    }
                    //if (L_L3MATERIAL_Ins.Count() > 0)
                    //{
                    //    idatabase.InsertRange<L3MATERIAL>(L_L3MATERIAL_Ins);
                    //}

                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }
    }
}
