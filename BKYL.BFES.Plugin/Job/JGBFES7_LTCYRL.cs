using BFES.DataAccess;
using BFES.Models;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using BKYL.BFES.Plugin.Models.JG;
using System.Reflection;

namespace BKYL.BFES.Plugin
{
    /// <summary>
    /// 同步报表系统酒钢原燃料数据
    /// </summary>
    class JGBFES7_LTCYRL : BaseJob
    {
        List<L3MATERIAL> l3materail = new List<L3MATERIAL>();
        DateTime calTime = new DateTime();
        public override void InitTask()
        {

        }
        public override void RunTask(DateTime currentTime)
        {
            //7JT,7GP,4GS,SSQ
            DateTime dt = currentTime.ToString("yyyy-MM-dd").ToDate();
            calTime = currentTime;
            GetL3material();

            List<L3ANALYSI> result = new List<L3ANALYSI>();
            result.AddRange(DealChangeMat(dt));
            result.AddRange(DealPublicMat());
            try
            {
                using (IDataBase idataBase = DalFactory.GreateIDataBase(base.S_DBSource))
                {
                    if (result.Count > 0)
                    {
                        foreach (L3ANALYSI item in result)
                        {
                            idataBase.Insert<L3ANALYSI>(item);
                        }
                        Outputlog($"L3ANALYSI插入{result.Count}条记录！\r\n");
                    }
                }
            }
            catch (Exception e)
            {

                throw;
            }
            finally
            {
                result.Clear();
                result = null;
            }

        }
        /// <summary>
        /// 处理焦炭、煤粉、球团、烧结 每天会更新
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<L3ANALYSI> DealChangeMat(DateTime dt)
        {
            List<L3ANALYSI> L3_list = new List<L3ANALYSI>();


            List<VIEW_BB_YL> YL_list = new List<VIEW_BB_YL>();
            List<VIEW_BB_RL> RL_list = new List<VIEW_BB_RL>();

            List<string> keyList = new List<string>();
            keyList.AddRange(GetUserc1ByKeyFromL3ana(dt, "SSQ"));
            keyList.AddRange(GetUserc1ByKeyFromL3ana(dt, "4GS"));
            keyList.AddRange(GetUserc1ByKeyFromL3ana(dt, "7JT"));
            keyList.AddRange(GetUserc1ByKeyFromL3ana(dt, "7GP"));

            YL_list.AddRange(GetVIEW_BB_YL(dt, "4GS"));
            YL_list.AddRange(GetVIEW_BB_YL(dt, "SSQ"));

            RL_list.AddRange(GetVIEW_BB_RL(dt, "7JT"));
            RL_list.AddRange(GetVIEW_BB_RL(dt, "7GP"));

            //对比l3表中已经插入的
            if (YL_list.Count > 0 && keyList.Count > 0)
            {
                FilterNewModel(ref YL_list, keyList);
                FilterNewModel(ref RL_list, keyList);
            }

            if (YL_list.Count > 0)
            {
                L3ANALYSI temp = null;
                foreach (VIEW_BB_YL item in YL_list)
                {
                    temp = ConvertBB_YL(item);
                    if (temp != null)
                    {
                        L3_list.Add(temp);
                    }
                }
            }
            if (RL_list.Count > 0)
            {
                L3ANALYSI temp = null;
                foreach (VIEW_BB_RL item in RL_list)
                {
                    temp = ConvertBB_RL(item);
                    if (temp != null)
                    {
                        L3_list.Add(temp);
                    }
                }
            }
            return L3_list;
        }
        /// <summary>
        /// 处理公共原料部分
        /// </summary>
        /// <returns></returns>
        private List<L3ANALYSI> DealPublicMat()
        {
            List<L3ANALYSI> L3_list = new List<Models.JG.L3ANALYSI>();

            List<L3ANA_BKYL_RL> OldRlList = new List<L3ANA_BKYL_RL>();
            List<L3ANA_BKYL_RL> NewRlList = new List<L3ANA_BKYL_RL>();
            List<L3ANA_BKYL_YRL> OldYRlList = new List<L3ANA_BKYL_YRL>();
            List<L3ANA_BKYL_YRL> NewYRlList = new List<L3ANA_BKYL_YRL>();

            List<L3ANA_BKYL_YRL> DiffYRlList = new List<L3ANA_BKYL_YRL>();
            List<L3ANA_BKYL_RL> DiffRlList = new List<L3ANA_BKYL_RL>();

            OldRlList = GetL3ANA_BKYL_RL();
            NewRlList = GetOriginal_BKYL_RL();

            OldYRlList = GetL3ANA_BKYL_YRL();;
            NewYRlList = GetOriginal_BKYL_YRL();


            DiffRlList = FindDiff(NewRlList, OldRlList);
            DiffYRlList = FindDiff(NewYRlList, OldYRlList);

            if (DiffRlList.Count > 0)
            {
                L3ANALYSI temp = null;
                foreach (L3ANA_BKYL_RL item in DiffRlList)
                {
                    temp = ConvertBKYL_RL(item);
                    if (temp != null)
                    {
                        L3_list.Add(temp);
                    }
                }
            }
            if (DiffYRlList.Count > 0)
            {
                L3ANALYSI temp = null;
                foreach (L3ANA_BKYL_YRL item in DiffYRlList)
                {
                    temp = ConvertBKYL_YRL(item);
                    if (temp != null)
                    {
                        L3_list.Add(temp);
                    }
                }
            }
            //同步公共原料表

            using (IDataBase idataBase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                if (DiffRlList.Count > 0)
                {
                    foreach (L3ANA_BKYL_RL item in DiffRlList)
                    {
                        idataBase.ExecuteCommand($"delete from L3ANA_BKYL_RL where m_name = '{item.M_NAME}'");
                        idataBase.Insert<L3ANA_BKYL_RL>(item);
                    }
                    Outputlog($"L3ANA_BKYL_RL更新{DiffRlList.Count}条记录！\r\n");
                }
                if (DiffYRlList.Count > 0)
                {
                    foreach (L3ANA_BKYL_YRL item in DiffYRlList)
                    {
                        idataBase.ExecuteCommand($"delete from L3ANA_BKYL_YRL where m_name = '{item.M_NAME}'");
                        idataBase.Insert<L3ANA_BKYL_YRL>(item);
                    }
                    Outputlog($"L3ANA_BKYL_YRL更新{DiffYRlList.Count}条记录！\r\n");
                }
            }
            return L3_list;
        }
        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            //输入错误到界面
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            //输出错误到日志
            Writelog(exception.Message + exception.StackTrace);
        }
        /// <summary>
        /// 读取原料成分
        /// </summary>
        /// <param name="dtTime"></param>
        /// <param name="key">试样关键字</param>
        /// <returns></returns>
        public List<VIEW_BB_YL> GetVIEW_BB_YL(DateTime dt, string key)
        {
            List<VIEW_BB_YL> modelList = new List<Plugin.VIEW_BB_YL>();
            string sql = "";
            using (IDataBase idataBase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                sql = "select  \"PASTRTERM\",  \"USERC1\"," +
                        "substr(\"TFE\"，41) TFE," +
                        "substr(\"FEO\"，41) FEO," +
                        "substr(\"SIO2\"，41) SIO2," +
                        "substr(\"CAO\"，41) CAO," +
                        "substr(\"S\"，41) S," +
                        "substr(\"P\"，41) P," +
                        "substr(\"R\"，41) R," +
                        "substr(\"TIO2\"，41) TIO2," +
                        "substr(\"K2O\"，41) K2O," +
                        "substr(\"NA2O\"，41) NA2O," +
                        "substr(\"ZNO\"，41) ZNO," +
                        "substr(\"转鼓\"，41) 转鼓," +
                        "substr(\"抗压强度\"，41) 抗压强度" +
                        $" from sa.\"View_BB_Yl\"@REPORT where \"PASTRTERM\" between { DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} -1 and  { DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}   and  \"USERC1\" like '{key}%'";

                return idataBase.GetList<VIEW_BB_YL>(sql).ToList();

            }
        }
        /// <summary>
        /// 读取燃料成分
        /// </summary>
        /// <param name="dtTime"></param>
        /// <param name="key">试样关键字</param>
        /// <returns></returns>
        public List<VIEW_BB_RL> GetVIEW_BB_RL(DateTime dt, string key)
        {
            List<VIEW_BB_RL> modelList = new List<Plugin.VIEW_BB_RL>();
            string sql = "";
            using (IDataBase idataBase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                sql = "select  \"PASTRTERM\",  \"USERC1\"," +
                        "substr(\"MT\"，41) MT," +
                        "substr(\"AD\"，41) AD," +
                        "substr(\"VDAF\"，41) VDAF," +
                        "substr(\"STD\"，41) STD," +
                        "substr(\"M40\"，41) M40," +
                        "substr(\"M10\"，41) M10," +
                        "substr(\"FCAD\"，41) FCAD," +
                        "substr(\"CRI\"，41) CRI," +
                        "substr(\"CSR\"，41) CSR" +
                        $" from sa.\"View_BB_Rl\"@REPORT where \"PASTRTERM\" between { DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} -1 and  { DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}  and \"USERC1\" like '{key}%'";

                return idataBase.GetList<VIEW_BB_RL>(sql).ToList();
            }
        }
        /// <summary>
        /// 获取最近的检化验试样号
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<string> GetUserc1ByKeyFromL3ana(DateTime dt, string key)
        {
            string sql = "";
            using (IDataBase idataBase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                sql = $"select USERC1 from L3ANALYSI where M_PASTTIME between { DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} -1 and  { DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}  and \"USERC1\" like '{key}%'";
                return idataBase.GetList<string>(sql).ToList();
            }

        }
        public L3ANALYSI ConvertBB_YL(VIEW_BB_YL model)
        {

            L3ANALYSI result = null;
            string Hour = "";

            if (model != null)
            {
                result = new L3ANALYSI();
                int index = -1;
                index = model.USERC1.IndexOf('-');
                if (index > 0)
                {
                    Hour = model.USERC1.Substring(index + 1);
                    result.TIMESTAMP = model.PASTRTERM.AddHours(Hour.ToDouble(0));
                }
                else
                {
                    //如果USERC1中填写错误不处理
                    return null;
                }
                //竖球
                if (model.USERC1.Contains("SSQ"))
                {
                    result.M_CODE = 60;
                }
                //4烧
                if (model.USERC1.Contains("4GS"))
                {
                    result.M_CODE = 51;
                }
                result.USERC1 = model.USERC1;
                result.M_CODE3 = result.M_CODE.ToString();
                result.M_TFE_CONT = model.TFE.ToDouble();
                result.M_FEO_CONT = model.FEO.ToDouble();
                result.M_SIO2_CONT = model.SIO2.ToDouble();
                result.M_CAO_CONT = model.CAO.ToDouble();
                result.M_S_CONT = model.S.ToDouble();
                result.M_P_CONT = model.P.ToDouble();
                result.M_BASICITY = model.R.ToDouble();
                result.M_TIO2_CONT = model.TIO2.ToDouble();
                result.M_K2O_CONT = model.K2O.ToDouble();
                result.M_NA2O_CONT = model.NA2O.ToDouble();
                result.M_ZN_CONT = model.ZNO.ToDouble();
                result.M_DI = model.转鼓.ToDouble(0);
                result.M_KYN = model.抗压强度.ToDouble();
                result.M_PASTTIME = model.PASTRTERM;
            }
            return result;
        }
        public L3ANALYSI ConvertBB_RL(VIEW_BB_RL model)
        {

            L3ANALYSI result = null;
            string Hour = "";

            if (model != null)
            {
                result = new L3ANALYSI();
                int index = -1;
                index = model.USERC1.IndexOf('-');
                if (index > 0)
                {
                    Hour = model.USERC1.Substring(index + 1);
                    result.TIMESTAMP = model.PASTRTERM.AddHours(Hour.ToDouble(0));
                }
                else
                {
                    //如果USERC1中填写错误不处理
                    return null;
                }
                //焦炭
                if (model.USERC1.Contains("7JT"))
                {
                    result.M_CODE = 33;
                }
                //煤粉
                if (model.USERC1.Contains("7GP"))
                {
                    result.M_CODE = 14;
                }
                result.USERC1 = model.USERC1;
                result.M_CODE3 = result.M_CODE.ToString();
                result.M_MOISTURE = model.MT.ToDouble();
                result.M_COKEASH = model.AD.ToDouble();
                result.M_VOLA_CONT = model.VDAF.ToDouble();
                result.M_S_CONT = model.STD.ToDouble();
                result.M_M40 = model.M40.ToDouble();
                result.M_M10 = model.M10.ToDouble();
                result.M_C_CONT = model.FCAD.ToDouble();
                result.M_CRI = model.CRI.ToDouble();
                result.M_CSR = model.CSR.ToDouble();
                result.M_PASTTIME = model.PASTRTERM;

            }
            return result;
        }
        /// <summary>
        /// 获取本次需要处理的model
        /// </summary>
        /// <param name="list"></param>
        /// <param name="KeyList"></param>
        public void FilterNewModel(ref List<VIEW_BB_YL> list, List<string> KeyList)
        {
            List<VIEW_BB_YL> result = new List<VIEW_BB_YL>();
            if (list != null && list.Count > 0)
            {
                foreach (VIEW_BB_YL item in list)
                {
                    if (KeyList.IndexOf(item.USERC1) < 0)
                    {
                        result.Add(item);
                    }
                }
            }
            list.Clear();
            list.AddRange(result);
        }
        /// <summary>
        /// 获取本次需要处理的model
        /// </summary>
        /// <param name="list"></param>
        /// <param name="KeyList"></param>
        public void FilterNewModel(ref List<VIEW_BB_RL> list, List<string> KeyList)
        {
            List<VIEW_BB_RL> result = new List<VIEW_BB_RL>();
            if (list != null && list.Count > 0)
            {
                foreach (VIEW_BB_RL item in list)
                {
                    if (KeyList.IndexOf(item.USERC1) < 0)
                    {
                        result.Add(item);
                    }
                }
            }
            list.Clear();
            list.AddRange(result);
        }
        public List<L3ANA_BKYL_RL> GetL3ANA_BKYL_RL()
        {
            string sql = "";
            using (IDataBase idataBase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                sql = "select * from L3ANA_BKYL_RL";
                return idataBase.GetList<L3ANA_BKYL_RL>(sql).ToList();
            }
        }
        public List<L3ANA_BKYL_YRL> GetL3ANA_BKYL_YRL()
        {
            string sql = "";
            using (IDataBase idataBase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                sql = "select * from L3ANA_BKYL_YRL";
                return idataBase.GetList<L3ANA_BKYL_YRL>(sql).ToList();
            }
        }
        /// <summary>
        /// 获取报表系统中的公共原料
        /// </summary>
        /// <returns></returns>
        public List<L3ANA_BKYL_YRL> GetOriginal_BKYL_YRL()
        {
            string sql = "";
            using (IDataBase idataBase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                sql = " select \"Name\" as M_NAME, " +
                        "  ROUND(\"TFeFixWet\", 5) as M_TFE_CONT, " +
                       "   ROUND(\"FeOFixWet\", 5) as M_FEO_CONT, " +
                       "   ROUND(\"SiO2FixWet\", 5) as M_SIO2_CONT, " +
                       "   ROUND(\"Al2O3FixWet\", 5) as M_AL2O3_CONT, " +
                       "   ROUND(\"CaOFixWet\", 5) as M_CAO_CONT, " +
                        "  ROUND(\"MgOFixWet\", 5) as M_MGO_CONT, " +
                       "   ROUND(\"Na2OFixWet\", 5) as M_NA2O_CONT, " +
                       "   ROUND(\"K2OFixWet\", 5) as M_K2O_CONT, " +
                       "   ROUND(\"SFixWet\", 5) as M_S_CONT, " +
                       "   ROUND(\"PFixWet\", 5) as M_P_CONT, " +
                        "  ROUND(\"TiO2FixWet\", 5) as M_TIO2_CONT, " +
                        "  ROUND(\"ZnOFixWet\", 5) as M_ZN_CONT, " +
                       "   ROUND(\"MnOFixWet\", 5) as M_MNO_CONT " +
           "  from sa.\"Bkyl_Yrl\"@report t";
                // sql = "select * from L3ANA_BKYL_YRL";
                return idataBase.GetList<L3ANA_BKYL_YRL>(sql).ToList();
            }
        }
        /// <summary>
        /// 获取报表系统中的公共燃料
        /// </summary>
        /// <returns></returns>
        public List<L3ANA_BKYL_RL> GetOriginal_BKYL_RL()
        {
            string sql = "";
            using (IDataBase idataBase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                sql = " select \"Name\" as M_NAME," +
                       "   ROUND(\"AdFixWet\", 5) as M_COKEASH, " +
                       "   ROUND(\"Al2O3FixWet\", 5) as M_AL2O3_CONT, " +
                       "   ROUND(\"CaOFixWet\", 5) as M_CAO_CONT, " +
                       "   ROUND(\"FCadFixWet\", 5) as M_C_CONT, " +
                       "   ROUND(\"Fe2O3FixWet\", 5) as M_FE2O3_CONT, " +
                       "   ROUND(\"TFeFixWet\", 5) as M_TFE_CONT, " +
                       "   ROUND(\"K2OFixWet\", 5) as M_K2O_CONT, " +
                       "   ROUND(\"MgOFixWet\", 5) as M_MGO_CONT, " +
                       "   ROUND(\"MnOFixWet\", 5) as M_MNO_CONT, " +
                       "   ROUND(\"SiO2FixWet\", 5) as M_SIO2_CONT, " +
                       "   ROUND(\"MtFixWet\", 5) as M_MOISTURE, " +
                        "  ROUND(\"Na2OFixWet\", 5) as M_NA2O_CONT, " +
                        "  ROUND(\"P2O5FixWet\", 5) as M_P2O5_CONT, " +
                       "   ROUND(\"SO3FixWet\", 5) as M_SO2_CONT, " +
                       "   ROUND(\"StdFixWet\", 5) as M_S_CONT, " +
                       "   ROUND(\"TiO2FixWet\", 5) as M_TIO2_CONT, " +
                       "   ROUND(\"ZnOFixWet\", 5) as M_ZN_CONT, " +
                       "   ROUND(\"VdafFixWet\", 5) as M_VOLA_CONT " +
                  "   from sa.\"Bkyl_Rl\"@report";

                //  sql = "select * from L3ANA_BKYL_RL";
                return idataBase.GetList<L3ANA_BKYL_RL>(sql).ToList();

            }
        }
        /// <summary>
        /// 比较两个list返回NewList中 与OldList不相同的对象集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="NewList"></param>
        /// <param name="OldList"></param>
        /// <returns></returns>
        private bool IsEqual<T>(T NewModel, T OldModel)
        {
            bool Equal = true;
            Type t = typeof(T);
            PropertyInfo[] props = t.GetProperties();
            //燃料只对比RLProp中的属性
            if (NewModel != null && OldModel != null)
            {
                foreach (var item in props)
                {
                    if (item.GetValue(OldModel, null).ToDouble() != item.GetValue(NewModel, null).ToDouble())
                    {
                        Equal = false;
                        break;
                    }
                }
            }
            return Equal;
        }
        private List<L3ANA_BKYL_RL> FindDiff(List<L3ANA_BKYL_RL> NewList, List<L3ANA_BKYL_RL> OldList)
        {
            List<L3ANA_BKYL_RL> result = new List<L3ANA_BKYL_RL>();
            if (NewList.Count > 0)
            {
                if (OldList.Count > 0)
                {
                    foreach (L3ANA_BKYL_RL item in NewList)
                    {
                        int index = OldList.FindIndex(m => m.M_NAME == item.M_NAME);
                        //如果旧的里边没有新的，或者新的与旧的属性值不同
                        if (index < 0 || (index >= 0 && !IsEqual<L3ANA_BKYL_RL>(item, OldList[index])))
                        {
                            result.Add(item);
                        }
                    }
                }
            }
            return result;
        }
        private List<L3ANA_BKYL_YRL> FindDiff(List<L3ANA_BKYL_YRL> NewList, List<L3ANA_BKYL_YRL> OldList)
        {
            List<L3ANA_BKYL_YRL> result = new List<L3ANA_BKYL_YRL>();
            if (NewList.Count > 0)
            {
                if (OldList.Count > 0)
                {
                    foreach (L3ANA_BKYL_YRL item in NewList)
                    {
                        int index = OldList.FindIndex(m => m.M_NAME == item.M_NAME);
                        //如果旧的里边没有新的，或者新的与旧的属性值不同
                        if (index < 0 || (index >= 0 && !IsEqual<L3ANA_BKYL_YRL>(item, OldList[index])))
                        {
                            result.Add(item);
                        }
                    }
                }
            }
            return result;
        }
        private void GetL3material()
        {
            using (IDataBase idataBase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                l3materail = idataBase.GetList<L3MATERIAL>("select * from L3MATERIAL");
            }
        }
        /// <summary>
        /// 转换公共燃料到L3ANALYSI
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private L3ANALYSI ConvertBKYL_RL(L3ANA_BKYL_RL model)
        {
            L3ANALYSI result = null;
            if (model != null)
            {
                int index = l3materail.FindIndex(m => m.NAME == model.M_NAME);
                if (index >= 0)
                {
                    result = new L3ANALYSI();
                    result.TIMESTAMP = calTime;
                    result.M_CODE = l3materail[index].CODE;
                    result.M_CODE3 = l3materail[index].CODEL3;
                    result.M_COKEASH = model.M_COKEASH;
                    result.M_AL2O3_CONT = model.M_AL2O3_CONT;
                    result.M_CAO_CONT = model.M_CAO_CONT;
                    result.M_C_CONT = model.M_C_CONT;
                    result.M_FE2O3_CONT = model.M_FE2O3_CONT;
                    result.M_TFE_CONT = model.M_TFE_CONT;
                    result.M_K2O_CONT = model.M_K2O_CONT;
                    result.M_MGO_CONT = model.M_MGO_CONT;
                    result.M_MNO_CONT = model.M_MNO_CONT;
                    result.M_SIO2_CONT = model.M_SIO2_CONT;
                    result.M_MOISTURE = model.M_MOISTURE;
                    result.M_NA2O_CONT = model.M_NA2O_CONT;
                    result.M_P2O5_CONT = model.M_P2O5_CONT;
                    result.M_SO2_CONT = model.M_SO2_CONT;
                    result.M_S_CONT = model.M_S_CONT;
                    result.M_TIO2_CONT = model.M_TIO2_CONT;
                    result.M_ZN_CONT = model.M_ZN_CONT;
                    result.M_VOLA_CONT = model.M_VOLA_CONT;
                }
            }
            return result;
        }
        /// <summary>
        /// 转换公共原料到L3ANALYSI
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private L3ANALYSI ConvertBKYL_YRL(L3ANA_BKYL_YRL model)
        {
            L3ANALYSI result = null;
            if (model != null)
            {
                int index = l3materail.FindIndex(m => m.NAME == model.M_NAME);
                if (index >= 0)
                {
                    result = new L3ANALYSI();
                    result.TIMESTAMP = calTime;
                    result.M_CODE = l3materail[index].CODE;
                    result.M_CODE3 = l3materail[index].CODEL3;
                    result.M_TFE_CONT = model.M_TFE_CONT;
                    result.M_FEO_CONT = model.M_FEO_CONT;
                    result.M_SIO2_CONT = model.M_SIO2_CONT;
                    result.M_AL2O3_CONT = model.M_AL2O3_CONT;
                    result.M_CAO_CONT = model.M_CAO_CONT;
                    result.M_MGO_CONT = model.M_MGO_CONT;
                    result.M_NA2O_CONT = model.M_NA2O_CONT;
                    result.M_K2O_CONT = model.M_K2O_CONT;
                    result.M_S_CONT = model.M_S_CONT;
                    result.M_P_CONT = model.M_P_CONT;
                    result.M_TIO2_CONT = model.M_TIO2_CONT;
                    result.M_ZN_CONT = model.M_ZN_CONT;
                    result.M_MNO_CONT = model.M_MNO_CONT;


                }
            }
            return result;
        }

    }
    public class VIEW_BB_YL
    {
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime PASTRTERM { get; set; }
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string USERC1 { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TFE { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string FEO { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SIO2 { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CAO { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string S { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string P { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string R { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TIO2 { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string K2O { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NA2O { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ZNO { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string 转鼓 { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string 抗压强度 { get; set; }

    }
    public class VIEW_BB_RL
    {
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime PASTRTERM { get; set; }
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string USERC1 { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string AD { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string VDAF { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string STD { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string M40 { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string M10 { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string FCAD { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CRI { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CSR { get; set; }

    }

    public class L3ANA_BKYL_RL
    {

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string M_NAME { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_COKEASH { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_AL2O3_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_CAO_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_C_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_FE2O3_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_TFE_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_K2O_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_MGO_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_MNO_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_SIO2_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_MOISTURE { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_NA2O_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_P2O5_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_SO2_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_S_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_TIO2_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_ZN_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_VOLA_CONT { get; set; }

    }

    public class L3ANA_BKYL_YRL
    {

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string M_NAME { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_TFE_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_FEO_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_SIO2_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_AL2O3_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_CAO_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_MGO_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_NA2O_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_K2O_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_S_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_P_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_TIO2_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_ZN_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_MNO_CONT { get; set; }

    }


}
namespace BKYL.BFES.Plugin.Models.JG
{

    public class L3MATERIAL
    {
        /// <summary>
        /// Desc:记录时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP { get; set; }

        /// <summary>
        /// Desc:料种编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_TYPE { get; set; }

        /// <summary>
        /// Desc:一级物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CODEL1 { get; set; }

        /// <summary>
        /// Desc:二级系统内部物料编码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int CODE { get; set; }

        /// <summary>
        /// Desc:三级物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CODEL3 { get; set; }

        /// <summary>
        /// Desc:物料名称 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// Desc:物料堆比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double DENSITY { get; set; }

        /// <summary>
        /// Desc:物料水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double MOISTURE { get; set; }

        /// <summary>
        /// Desc:物料单价 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double PERVALUE { get; set; }
    }
}
