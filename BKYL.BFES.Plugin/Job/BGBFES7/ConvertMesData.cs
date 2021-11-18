using BFES.DataAccess;
using BKYL.BFES.Plugin.Models;
using BKYL.Jobs.Base.Utils;
using BKYL.Jobs.Base;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace BKYL.BFES.Plugin.BGBFES7
{

    class ConvertMesData : BaseJob
    {
        string log = "";
        List<CONFIG_L3ANA_ITEMMAP> ItemMapList = new List<CONFIG_L3ANA_ITEMMAP>();

        List<L3MATERIAL> UseL3Material = new List<L3MATERIAL>();
        Dictionary<string, string> dicL3ana = new Dictionary<string, string>();
        Dictionary<string, string> dicIron = new Dictionary<string, string>();
        Dictionary<string, string> dicSlag = new Dictionary<string, string>();
        int DealDay = 3; //第一次处理60天

        public override void InitTask()
        {
            List<L3MATERIAL> L3MaterialList = new List<L3MATERIAL>();
            //初始化检化验项目映射关系
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                ItemMapList = idatabase.Queryable<CONFIG_L3ANA_ITEMMAP>().ToList();
                L3MaterialList = idatabase.Queryable<L3MATERIAL>().ToList();
            }
            if (L3MaterialList.Count > 0)
            {
                UseL3Material = L3MaterialList.FindAll(match => match.CODEL3 != null);
            }
            if (UseL3Material.Count == 0)
            {
                return;
            }
            if (ItemMapList.Count > 0)
            {
                foreach (CONFIG_L3ANA_ITEMMAP item in ItemMapList)
                {
                    switch (item.M_TYPE)
                    {
                        case 1:
                            dicL3ana.Add(item.L2_ITEMFIELD, item.L3_ITEMFIELD);
                            break;
                        case 2:
                            dicIron.Add(item.L2_ITEMFIELD, item.L3_ITEMFIELD);
                            break;
                        case 3:
                            dicSlag.Add(item.L2_ITEMFIELD, item.L3_ITEMFIELD);
                            break;
                        default:
                            break;
                    }
                }
            }

        }
        public override void RunTask(DateTime currentTime)
        {
            log = "";
            //currentTime = "2019-07-31 23:00:00".ToDate();
            DateTime startTime = currentTime.AddDays(-DealDay);

            #region 处理原燃料           
            List<MES_UTIL_QUALITY_MES> SourceData = new List<BGBFES7.MES_UTIL_QUALITY_MES>();
            List<L3ANALYSI> L3anaList = new List<L3ANALYSI>();
            L3ANALYSI l3ana = null;
            foreach (L3MATERIAL item in UseL3Material)
            {
                SourceData = GetSourceData(item.CODEL3, startTime, currentTime);

                if (SourceData.Count > 0) //如果本次没有查询到料种的试样编码
                {
                    foreach (MES_UTIL_QUALITY_MES m in SourceData)
                    {
                        l3ana = new L3ANALYSI();
                        l3ana.M_CODE = item.CODE;
                        l3ana.M_CODE3 = item.CODEL3;
                        l3ana.M_SAMPLE_NO = m.SAMPLE_ID;
                        l3ana.M_SAMPLE_TIME = m.SAMPLE_TIME;
                        l3ana.TIMESTAMP = m.TEST_TIME;
                        GetModelValue(m, dicL3ana, ref l3ana);
                        if (item.M_TYPE == 2 || item.M_TYPE == 4 || item.M_TYPE == 5)
                        {
                            //固定碳=100-灰分-挥发分-水分-硫分
                            l3ana.M_C_CONT = 100 - (l3ana.M_COKEASH + l3ana.M_MOISTURE + l3ana.M_ST_D_VAL + l3ana.M_VOLA_CONT).ToDouble();
                        }
                        L3anaList.Add(l3ana);
                    }
                }
            }
            SourceData.Clear();
            SourceData = null;
            int delete = 0;
            log = "";
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                try
                {
                    if (L3anaList.Count > 0)
                    {
                        delete = idatabase.ExecuteCommand($"delete from L3ANALYSI where timestamp>={DataBaseFactory.ConvertTimeString(startTime, S_DBSource.DBType)} and timestamp<={DataBaseFactory.ConvertTimeString(currentTime, S_DBSource.DBType)}");
                        log += $"L3ANALYSI成功删除{delete}条记录\r\n";
                        L3anaList = DealRepeatData(L3anaList);
                        foreach (L3ANALYSI model in L3anaList)
                        {
                            idatabase.Insert<L3ANALYSI>(model);
                        }
                        log += $"L3ANALYSI成功插入{L3anaList.Count}条记录\r\n";
                    }
                    log += "L3ANALYSI处理完成";
                    Outputlog(log);
                }
                catch (Exception)
                {
                    idatabase.RollbackTran();
                    throw;
                }
                finally
                {
                    L3anaList.Clear();
                    L3anaList = null;
                }
            }
            #endregion

            #region 处理铁水           
            List<L3TAPPING_IRON_ANA> IronList = new List<L3TAPPING_IRON_ANA>();
            L3TAPPING_IRON_ANA Iron = null;
            SourceData = new List<BGBFES7.MES_UTIL_QUALITY_MES>();
            SourceData = GetSourceData("T", startTime, currentTime);
            if (SourceData.Count >= 0)
            {
                foreach (MES_UTIL_QUALITY_MES m in SourceData)
                {
                    if (m.PROCESS_UNIT_ABBR == "BF07")
                    {
                        Iron = new L3TAPPING_IRON_ANA() { TIMESTAMP = m.TEST_TIME, MAT_SAMPLE_NO = m.SAMPLE_ID, MAT_SAMPLE_TIME = m.SAMPLE_TIME };
                        Iron.TAPNUMB = m.SAMPLE_ID.Remove(8).Substring(3);
                        GetModelValue(m, dicIron, ref Iron);
                        IronList.Add(Iron);
                    }
                }
            }
            SourceData.Clear();
            SourceData = null;
            log = "";
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                try
                {
                    if (IronList.Count > 0)
                    {
                        delete = idatabase.ExecuteCommand($"delete from L3TAPPING_IRON_ANA where timestamp>={DataBaseFactory.ConvertTimeString(startTime, S_DBSource.DBType)} and timestamp<={DataBaseFactory.ConvertTimeString(currentTime, S_DBSource.DBType)}");
                        log += $"L3TAPPING_IRON_ANA成功删除{delete}条记录\r\n";
                        foreach (L3TAPPING_IRON_ANA model in IronList)
                        {
                            idatabase.Insert<L3TAPPING_IRON_ANA>(model);
                        }
                        log += $"L3TAPPING_IRON_ANA成功插入{IronList.Count}条记录\r\n";

                    }
                    log += "L3TAPPING_IRON_ANA处理完成";
                    Outputlog(log);
                }
                catch (Exception)
                {
                    idatabase.RollbackTran();
                    throw;
                }
                finally
                {
                    IronList.Clear();
                    IronList = null;
                }
            }
            #endregion

            #region 处理炉渣           
            List<L3TAPPING_SLAG_ANA> SlagList = new List<L3TAPPING_SLAG_ANA>();
            L3TAPPING_SLAG_ANA Slag = null;
            SourceData = new List<BGBFES7.MES_UTIL_QUALITY_MES>();
            SourceData = GetSourceData("Z", startTime, currentTime);
            if (SourceData.Count >= 0)
            {
                foreach (MES_UTIL_QUALITY_MES m in SourceData)
                {
                    if (m.PROCESS_UNIT_ABBR == "BF07")
                    {
                        Slag = new L3TAPPING_SLAG_ANA() { TIMESTAMP = m.TEST_TIME, MAT_SAMPLE_NO = m.SAMPLE_ID, MAT_SAMPLE_TIME = m.SAMPLE_TIME };
                        Slag.TAPNUMB = m.SAMPLE_ID.Remove(8).Substring(3);
                        GetModelValue(m, dicSlag, ref Slag);
                        if (Slag.SIO2 > 0)
                        {
                            Slag.R2 = Slag.CAO / Slag.SIO2;
                            Slag.R3 = (Slag.CAO + Slag.MGO) / Slag.SIO2;
                            Slag.R4 = (Slag.CAO + Slag.MGO) / (Slag.SIO2 + Slag.AL2O3);
                            Slag.CAO_2SIO2 = Slag.CAO / Slag.SIO2;
                        }
                        if (Slag.AL2O3 > 0)
                        {
                            Slag.MGO_AL2O3 = Slag.MGO / Slag.AL2O3;
                        }
                        SlagList.Add(Slag);
                    }
                }
            }
            SourceData.Clear();
            SourceData = null;
            log = "";
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                try
                {
                    if (SlagList.Count > 0)
                    {
                        delete = idatabase.ExecuteCommand($"delete from L3TAPPING_SLAG_ANA where timestamp>={DataBaseFactory.ConvertTimeString(startTime, S_DBSource.DBType)} and timestamp<={DataBaseFactory.ConvertTimeString(currentTime, S_DBSource.DBType)}");
                        log += $"L3TAPPING_SLAG_ANA成功删除{delete}条记录\r\n";
                        foreach (L3TAPPING_SLAG_ANA model in SlagList)
                        {
                            idatabase.Insert<L3TAPPING_SLAG_ANA>(model);
                        }
                        log += $"L3TAPPING_SLAG_ANA成功插入{SlagList.Count}条记录\r\n";
                    }
                    log += "L3TAPPING_SLAG_ANA处理完成";
                    Outputlog(log); 
                }
                catch (Exception)
                {
                    idatabase.RollbackTran();
                    throw;
                }
                finally
                {
                    SlagList.Clear();
                    SlagList = null;
                }
            }
            #endregion
            
        }
        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            //输入错误到界面
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            //输出错误到日志
            Writelog(exception.Message + exception.StackTrace);
        }
        /// <summary>
        /// 读取数据源
        /// </summary>
        /// <param name="key">3级物料编码</param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        private List<MES_UTIL_QUALITY_MES> GetSourceData(string key, DateTime startTime, DateTime endTime)
        {
            List<MES_UTIL_QUALITY_MES> result = new List<MES_UTIL_QUALITY_MES>();
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                result = idatabase.Queryable<MES_UTIL_QUALITY_MES>().Where(m => m.TEST_TIME > startTime && m.TEST_TIME <= endTime && m.SAMPLE_MTRL_CODE == key).ToList();
            }
            return result;
        }
        private void GetModelValue<T>(MES_UTIL_QUALITY_MES SourceData, Dictionary<string, string> Dic, ref T Model)
        {

            Type t = typeof(T);
            Type souceT = SourceData.GetType();
            if (SourceData != null)
            {
                foreach (var item in Dic)
                {
                    if (t.GetProperty(item.Key) != null && souceT.GetProperty(item.Value) != null)
                    {
                        try
                        {
                            t.GetProperty(item.Key).SetValue(Model, souceT.GetProperty(item.Value).GetValue(SourceData, null).ToDouble(0), null);
                        }
                        catch (Exception e)
                        {
                            log += $"表CONFIG_L3ANA_ITEMMAP中L3_ITEMFIELD列“{item.Value}”非值类型\r\n";
                        }


                    }
                }
            }
        }
        //修改同一个料种一个时间点两个样的问题，将时间加一秒
        private List<L3ANALYSI> DealRepeatData(List<L3ANALYSI> ListModel)
        {
            List<L3ANALYSI> listtemp = new List<L3ANALYSI>();
            if (ListModel != null && ListModel.Count > 0)
            {
                ListModel = ListModel.OrderBy(x => x.TIMESTAMP).ToList();

                foreach (var item in ListModel)
                {
                    if (listtemp.Find(t => t.M_CODE == item.M_CODE && t.TIMESTAMP == item.TIMESTAMP) != null)
                    {
                        item.TIMESTAMP = listtemp.FindAll(x => x.M_CODE == item.M_CODE).Max(match => match.TIMESTAMP).AddSeconds(1);
                    }
                    listtemp.Add(item);
                }
            }
            return listtemp;
        }
    }
}
