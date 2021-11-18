using BFES.DataAccess;
using BFES.DataAccess.Common;
using BKYL.BFES.Plugin.BGBFES7;
using BKYL.BFES.Plugin.Models;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin
{
    public class Job_CalHearthBalanceData_BG7 : BaseJob
    {
        List<TAPPING> tappingList = new List<TAPPING>();
        List<BATCHHIS> batchhisList = new List<BATCHHIS>();
        List<HEARTHBALANCE_IRONDIFF> TapHearthIronDiff = new List<HEARTHBALANCE_IRONDIFF>();
        List<HEARTHBALANCE_IRONDIFF_CLASS> ClassHearthIronDiff = new List<HEARTHBALANCE_IRONDIFF_CLASS>();
        List<HEARTHBALANCE_IRONDIFF_DAY> DayHearthIronDiff = new List<HEARTHBALANCE_IRONDIFF_DAY>();
        List<DividBatchhis> BatDivList = new List<DividBatchhis>();
        Dictionary<string, string> batDic = new Dictionary<string, string>();
        public override void InitTask()
        {
            batDic.Add("A_CODE1", "A_AMOUNT1");
            batDic.Add("A_CODE2", "A_AMOUNT2");
            batDic.Add("A_CODE3", "A_AMOUNT3");
            batDic.Add("A_CODE4", "A_AMOUNT4");
            batDic.Add("A_CODE5", "A_AMOUNT5");
            batDic.Add("A_CODE6", "A_AMOUNT6");
            batDic.Add("A_CODE7", "A_AMOUNT7");
            batDic.Add("A_CODE8", "A_AMOUNT8");
            batDic.Add("A_CODE9", "A_AMOUNT9");
            return;
        }
        public override void RunTask(DateTime currentTime)
        {
            string log = "";
            int CalNum = 7;
            DateTime startTime = currentTime.AddDays(-CalNum);


            tappingList = GetNewTapping(startTime, currentTime);
            tappingList = tappingList.GroupBy(x => x.TAPNUMB).Select(x => x.OrderBy(y => y.TIMESTAMP).Last()).ToList();
            batchhisList = GetBatchhis(startTime, currentTime);

            int delete = 0;
            //#region 计算一天的累计物料值
            //string[] columnNamesBatchhisTotal = { "TIMESTAMP", "CODE", "WEIGHT" };
            //string[] dataFiledNameArrBatchhisTotal = { "TIMESTAMP", "CODE", "WEIGHT" };
            //List<BATCHHIS_TOTAL> tempBatchhisTotal = null;
            //List<BATCHHIS_TOTAL> BatchhisTotalList = new List<BATCHHIS_TOTAL>();

            //for (int i = 0; i < CalNum; i++)
            //{
            //    tempBatchhisTotal = CalBatchhisTotal(currentTime.AddDays(-i));
            //    if (tempBatchhisTotal != null && tempBatchhisTotal.Count > 0)
            //    {
            //        BatchhisTotalList.AddRange(tempBatchhisTotal);
            //    }
            //}
            //if (BatchhisTotalList.Count > 0)
            //{
            //    DateTime max = BatchhisTotalList.Max(m => m.TIMESTAMP);
            //    DateTime min = BatchhisTotalList.Min(m => m.TIMESTAMP);
            //    using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            //    {
            //        try
            //        {
            //            idatabase.BeginTran();
            //            //idatabase.Delete(TapModelList);
            //            delete = idatabase.ExecuteCommand($"delete from BATCHHIS_TOTAL where TIMESTAMP>={DataBaseFactory.ConvertTimeString(min, S_DBSource.DBType)} and TIMESTAMP<={DataBaseFactory.ConvertTimeString(max, S_DBSource.DBType)}");
            //            idatabase.BulkInsert<BATCHHIS_TOTAL>("BATCHHIS_TOTAL", columnNamesBatchhisTotal, dataFiledNameArrBatchhisTotal, BatchhisTotalList);
            //            log += $"BATCHHIS_TOTAL更新最新{BatchhisTotalList.Count}条记录\r\n";
            //            idatabase.CommitTran();
            //            Outputlog(log);
            //        }
            //        catch (Exception)
            //        {
            //            idatabase.RollbackTran();
            //            throw;
            //        }
            //        finally
            //        {
            //            BatchhisTotalList.Clear();
            //            BatchhisTotalList = null;
            //        }
            //    }
            //}
            //#endregion


            #region 处理炉次盈亏铁量
            string[] columnNamesTap = { "TIMESTAMP", "BC", "BB", "TAPNUMB", "HOLENUMB", "TAP_START", "TAP_END", "IRON_THEORY", "IRON_REAL", "IRONDIFF", "SI", "HMTP", "TAP_SPEED", "SLAG_START", "IRONPRODRATIO", "SLAGPRODRATIO" };
            string[] dataFiledNameArrTap = { "TIMESTAMP", "BC", "BB", "TAPNUMB", "HOLENUMB", "TAP_START", "TAP_END", "IRON_THEORY", "IRON_REAL", "IRONDIFF", "SI", "HMTP", "TAP_SPEED", "SLAG_START", "IRONPRODRATIO", "SLAGPRODRATIO" };


            List<HEARTHBALANCE_IRONDIFF> TapModelList = new List<HEARTHBALANCE_IRONDIFF>();
            HEARTHBALANCE_IRONDIFF TapModel = new HEARTHBALANCE_IRONDIFF();
            DateTime timestamp = new DateTime();
            foreach (TAPPING item in tappingList)
            {
                timestamp = item.TAPSTART.ToDate();

                if (timestamp.Hour >= 20 && timestamp.Hour < 24)
                {
                    timestamp = timestamp.ToString("yyyy-MM-dd").ToDate().AddDays(1);
                }
                else
                {
                    timestamp = timestamp.ToString("yyyy-MM-dd").ToDate();
                }
                TapModel = new HEARTHBALANCE_IRONDIFF()
                {
                    TIMESTAMP = timestamp,
                    TAPNUMB = item.TAPNUMB.ToString(),
                    TAP_START = item.TAPSTART.ToDate(),
                    HOLENUMB = item.HOLENUMB,
                    TAP_END = item.TAPEND.ToDate(),
                    IRON_THEORY = item.IRON_WCAL.ToDouble(),
                    IRON_REAL = item.TAPWREAL.ToDouble(),
                    IRONDIFF = item.TAPDIFFE.ToDouble(),
                    TAP_SPEED = item.IRONSPEED.ToDouble(),
                    SI = item.HMSI.ToDouble(),
                    HMTP = item.HMTP,
                    SLAG_START = item.SLAGSTART.ToDate()
                };
                if ((TapModel.TAP_START.Hour >= 20 && TapModel.TAP_START.Hour <= 24) || (TapModel.TAP_START.Hour >= 0 && TapModel.TAP_START.Hour < 8))
                {
                    TapModel.BC = "夜班";
                }
                if (TapModel.TAP_START.Hour >= 8 && TapModel.TAP_START.Hour < 20)
                {
                    TapModel.BC = "白班";
                }

                if (item.TAPEND.ToDate() != DateTime.MinValue)
                {
                    DateTime LastEnd = tappingList.FindAll(m => m.TAPEND < item.TAPEND).Max(s => s.TAPEND).ToDate();
                    if (LastEnd.ToDate() != DateTime.MinValue)
                    {
                        TapModel.IRONPRODRATIO = (item.TAPEND.ToDate() - item.TAPSTART.ToDate()).TotalMinutes / (item.TAPEND.ToDate() - LastEnd.ToDate()).TotalMinutes * 100;
                    }
                }
                TapModel.SLAGPRODRATIO = (item.TAPEND.ToDate() - item.SLAGSTART.ToDate()).TotalMinutes / (item.TAPEND.ToDate() - item.TAPSTART.ToDate()).TotalMinutes * 100;
                //TapModel.IRON_THEORY = CalTappingTheroyIron(item.TAPNUMB.ToString());
                //TapModel.IRONDIFF = TapModel.IRON_REAL - TapModel.IRON_THEORY;
                //TapModel.TAP_SPEED = (TapModel.TAP_END != null && TapModel.TAP_START != null) ? (double?)(TapModel.IRON_THEORY / (((TapModel.TAP_END - TapModel.TAP_START).TotalSeconds / 3600.0))) : null;
                TapModelList.Add(TapModel);
            }
            TapHearthIronDiff = new List<HEARTHBALANCE_IRONDIFF>();
            TapHearthIronDiff.AddRange(TapModelList);

            DateTime end = TapModelList.Max(m => m.TAP_END);
            DateTime st = TapModelList.Min(m => m.TAP_END);

            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                try
                {
                    if (TapModelList.Count > 0)
                    {
                        idatabase.BeginTran();
                        //idatabase.Delete(TapModelList);
                        delete = idatabase.ExecuteCommand($"delete from HEARTHBALANCE_IRONDIFF where TAP_END>={DataBaseFactory.ConvertTimeString(st, S_DBSource.DBType)} and TAP_END<={DataBaseFactory.ConvertTimeString(end, S_DBSource.DBType)}");
                        log = $"HEARTHBALANCE_IRONDIFF成功删除{TapModelList.Count}条记录\r\n";
                        idatabase.BulkInsert<HEARTHBALANCE_IRONDIFF>("HEARTHBALANCE_IRONDIFF", columnNamesTap, dataFiledNameArrTap, TapModelList);
                        log += $"HEARTHBALANCE_IRONDIFF成功插入{TapModelList.Count}条记录\r\n";

                        //int iStart = TapModelList.Min(m => m.TAPNUMB.Substring(0, 5).ToInt());
                        //int iEnd = TapModelList.Max(m => m.TAPNUMB.Substring(0, 5).ToInt());
                        //delete = idatabase.ExecuteCommand($"delete from HEARTHBALANCE_IRONDIFF where to_number(substr(tapnumb,3,6))>= {iStart} and  to_number(substr(tapnumb,3,6))<={iEnd}");
                        //log = $"HEARTHBALANCE_IRONDIFF成功删除{delete}条记录\r\n";
                        //idatabase.BulkInsert<HEARTHBALANCE_IRONDIFF>("HEARTHBALANCE_IRONDIFF", columnNamesTap, dataFiledNameArrTap, TapModelList);
                        //log += $"HEARTHBALANCE_IRONDIFF成功插入{TapModelList.Count}条记录\r\n";

                        idatabase.CommitTran();
                        Outputlog(log);
                    }

                }
                catch (Exception)
                {
                    idatabase.RollbackTran();
                    throw;
                }
                finally
                {
                    TapModelList.Clear();
                    TapModelList = null;
                }
            }
            #endregion

            #region 处理班盈亏铁量
            CalClassDayDiffIron();
            end = ClassHearthIronDiff.Max(m => m.TIMESTAMP);
            st = ClassHearthIronDiff.Min(m => m.TIMESTAMP);
            string[] columnNamesClass = { "TIMESTAMP", "BC", "BB", "IRON_THEORY", "IRON_REAL", "IRONDIFF", "SI_AVG", "SI_DIFF", "YIELD", "COKEWEIGHT", "LITTLEWEIGHT", "SINTERWEIGHT", "PELLETWEIGHT", "FLUXWEIGHT", "CURDEWEIGHT", "IRONPRODRATIO", "SLAGPRODRATIO" };
            string[] dataFiledNameArrClass = { "TIMESTAMP", "BC", "BB", "IRON_THEORY", "IRON_REAL", "IRONDIFF", "SI_AVG", "SI_DIFF", "YIELD", "COKEWEIGHT", "LITTLEWEIGHT", "SINTERWEIGHT", "PELLETWEIGHT", "FLUXWEIGHT", "CURDEWEIGHT", "IRONPRODRATIO", "SLAGPRODRATIO" };

            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                try
                {
                    if (ClassHearthIronDiff.Count > 0)
                    {
                        // ClassHearthIronDiff.RemoveAt(0);
                        idatabase.BeginTran();
                        // idatabase.Delete(ClassHearthIronDiff);
                        delete = idatabase.ExecuteCommand($"delete from HEARTHBALANCE_IRONDIFF_CLASS where timestamp>={DataBaseFactory.ConvertTimeString(st, S_DBSource.DBType)} and timestamp<={DataBaseFactory.ConvertTimeString(end, S_DBSource.DBType)}");
                        log = $"HEARTHBALANCE_IRONDIFF_CLASS成功删除{ClassHearthIronDiff.Count}条记录\r\n";
                        idatabase.BulkInsert<HEARTHBALANCE_IRONDIFF_CLASS>("HEARTHBALANCE_IRONDIFF_CLASS", columnNamesClass, dataFiledNameArrClass, ClassHearthIronDiff);
                        log += $"HEARTHBALANCE_IRONDIFF_CLASS成功插入{ClassHearthIronDiff.Count}条记录\r\n";
                        idatabase.CommitTran();
                        Outputlog(log);
                    }

                }
                catch (Exception)
                {
                    idatabase.RollbackTran();
                    throw;
                }
                finally
                {
                    ClassHearthIronDiff.Clear();
                    ClassHearthIronDiff = null;
                }
            }
            #endregion

            #region 处理天盈亏铁量
            end = DayHearthIronDiff.Max(m => m.TIMESTAMP);
            st = DayHearthIronDiff.Min(m => m.TIMESTAMP);
            string[] columnNamesDay = { "TIMESTAMP", "IRON_THEORY", "IRON_REAL", "IRONDIFF", "SI_AVG", "SI_DIFF", "YIELD", "COKEWEIGHT", "LITTLEWEIGHT", "SINTERWEIGHT", "PELLETWEIGHT", "FLUXWEIGHT", "CURDEWEIGHT", "IRONPRODRATIO", "SLAGPRODRATIO" };
            string[] dataFiledNameArrDay = { "TIMESTAMP", "IRON_THEORY", "IRON_REAL", "IRONDIFF", "SI_AVG", "SI_DIFF", "YIELD", "COKEWEIGHT", "LITTLEWEIGHT", "SINTERWEIGHT", "PELLETWEIGHT", "FLUXWEIGHT", "CURDEWEIGHT", "IRONPRODRATIO", "SLAGPRODRATIO" };
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                try
                {
                    if (DayHearthIronDiff.Count > 0)
                    {
                        idatabase.BeginTran();
                        //idatabase.Delete(DayHearthIronDiff);
                        delete = idatabase.ExecuteCommand($"delete from HEARTHBALANCE_IRONDIFF_DAY where timestamp>={DataBaseFactory.ConvertTimeString(st, S_DBSource.DBType)} and timestamp<={DataBaseFactory.ConvertTimeString(end, S_DBSource.DBType)}");
                        log = $"HEARTHBALANCE_IRONDIFF_DAY成功删除{DayHearthIronDiff.Count}条记录\r\n";
                        idatabase.BulkInsert<HEARTHBALANCE_IRONDIFF_DAY>("HEARTHBALANCE_IRONDIFF_DAY", columnNamesDay, dataFiledNameArrDay, DayHearthIronDiff);
                        log += $"HEARTHBALANCE_IRONDIFF_DAY成功插入{DayHearthIronDiff.Count}条记录\r\n";
                        idatabase.CommitTran();
                        Outputlog(log);
                    }

                }
                catch (Exception)
                {
                    idatabase.RollbackTran();
                    throw;
                }
                finally
                {
                    TapHearthIronDiff.Clear();
                    TapHearthIronDiff = null;
                    tappingList.Clear();
                    tappingList = null;
                    batchhisList.Clear();
                    batchhisList = null;
                    BatDivList.Clear();
                    BatDivList = null;
                    DayHearthIronDiff.Clear();
                    DayHearthIronDiff = null;
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
        /// 读取铁水出铁信息
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        private List<TAPPING> GetNewTapping(DateTime startTime, DateTime endTime)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                return idatabase.Queryable<TAPPING>().Where(match => match.TAPSTART > startTime && match.TAPSTART < endTime).OrderBy(m => m.TAPNUMB, OrderByType.Desc).ToList();
            }
        }
        /// <summary>
        /// 读取铁水Si
        /// </summary>
        /// <param name="tapNumb"></param>
        /// <returns></returns>
        private List<BGBFES7.L3TAPPING_IRON_ANA> GetL3TAPPING_IRON_ANA(string tapNumb)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                return idatabase.Queryable<BGBFES7.L3TAPPING_IRON_ANA>().Where(match => match.TAPNUMB == tapNumb).ToList();
            }
        }
        /// <summary>
        /// bachhis多取一天的数据，因为时间最小的出铁信息记录的上一次出铁在startTime之前
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        private List<BATCHHIS> GetBatchhis(DateTime startTime, DateTime endTime)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                return idatabase.Queryable<BATCHHIS>().Where(match => match.TIMESTAMP > startTime.AddDays(-1) && match.TIMESTAMP <= endTime).OrderBy(m => m.TIMESTAMP, OrderByType.Desc).ToList();
            }
        }
        //获取铁次的理论出铁量
        private double CalTappingTheroyIron(string tapNumb)
        {
            string LastTapNumb = "";
            double Result = 0;
            LastTapNumb = TapNumb_its(TapNumb_sti(tapNumb) - 1);
            TAPPING model = new TAPPING();
            TAPPING Lastmodel = new TAPPING();
            if (tappingList.Count > 0)
            {
                int index = -1;
                index = tappingList.FindIndex(match => match.TAPNUMB.ToString() == tapNumb);
                if (index >= 0)
                {
                    model = tappingList[index];
                }
                index = -1;
                index = tappingList.FindIndex(match => match.TAPNUMB.ToString() == LastTapNumb);
                if (index >= 0)
                {
                    Lastmodel = tappingList[index];
                }
                else
                {
                    Lastmodel = GetLastTapping(LastTapNumb);
                }

            }
            if (Lastmodel != null)
            {
                Result = batchhisList.FindAll(match => match.TIMESTAMP > Lastmodel.TAPEND && match.TIMESTAMP <= model.TAPEND).Sum(m => m.A_MHTM).ToDouble();
            }
            return Result;
        }
        /// <summary>
        /// 字符串铁次号转数字
        /// </summary>
        /// <param name="tapNumb"></param>
        /// <returns></returns>
        private int TapNumb_sti(string tapNumb)
        {
            int IntTapNumb = 0;
            if (!string.IsNullOrEmpty(tapNumb))
            {
                IntTapNumb = tapNumb.Substring(2, tapNumb.Length - 2).ToInt(0);
            }
            return IntTapNumb;
        }
        /// <summary>
        /// 数字铁次号转字符串
        /// </summary>
        /// <param name="tapNumb"></param>
        /// <returns></returns>
        private string TapNumb_its(int tapNumb)
        {
            return string.Format("T6{0}", tapNumb);
        }
        /// <summary>
        /// 根据铁次号获取记录
        /// </summary>
        /// <param name="tapNumb"></param>
        /// <returns></returns>
        private TAPPING GetLastTapping(string tapNumb)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                return idatabase.GetSingle<TAPPING>($"select * from TAPPING where TAPNUMB ='{tapNumb}'");
            }
        }
        /// <summary>
        /// 本次铁次出铁对应的batchhis对象为：本次出铁时间与上次出铁结束时间之间的batchhis
        /// </summary>
        /// <param name="batList"></param>
        /// <returns></returns>
        private List<DividBatchhis> dividBatchhis(List<BATCHHIS> batList)
        {
            List<DividBatchhis> DividList = new List<DividBatchhis>();
            DividBatchhis dividModel = new DividBatchhis();

            List<BATCHHIS> tempList = batList.OrderByDescending(m => m.TIMESTAMP).ToList();

            for (int i = 0; i < tempList.Count; i++)
            {
                //如果是第一条记录先添加进来
                if (i == 0)
                {
                    dividModel = new DividBatchhis();
                    dividModel.batList = new List<BATCHHIS>();
                    dividModel.batList.Add(tempList[i]);
                }
                //如果料批号在减小，添加进来
                else if (tempList[i].A_CHARGES < tempList[i - 1].A_CHARGES)
                {
                    dividModel.batList.Add(tempList[i]);
                }
                //将上边统计的dividModel添加到DividList，如果料批号变大，从新初始化
                else
                {
                    #region 计算时间
                    List<DateTime> dtList = new List<DateTime>();
                    List<string> BBlist = new List<string>();
                    foreach (BATCHHIS item in dividModel.batList)
                    {
                        if (item.TIMESTAMP.Hour >= 20 && item.TIMESTAMP.Hour < 24)
                        {
                            BBlist.Add("夜班");
                            dtList.Add(item.TIMESTAMP.ToString("yyyy-MM-dd").ToDate().AddDays(1));
                        }
                        else if (item.TIMESTAMP.Hour >= 0 && item.TIMESTAMP.Hour < 8)
                        {
                            BBlist.Add("夜班");
                            dtList.Add(item.TIMESTAMP.ToString("yyyy-MM-dd").ToDate());
                        }
                        else
                        {
                            BBlist.Add("白班");
                            dtList.Add(item.TIMESTAMP.ToString("yyyy-MM-dd").ToDate());
                        }
                    }
                    //取班次个数出现最多的，防止异常数据
                    int count = BBlist.Count(match => match == "夜班");
                    if (count > dividModel.batList.Count * 0.9)
                    {
                        dividModel.BC = "夜班";
                    }
                    else
                    {
                        dividModel.BC = "白班";
                    }

                    //取时间相同个数最多的时间
                    count = dtList.Count(m => m == dtList[0]);
                    if (count > dtList.Count * 0.9)
                    {
                        dividModel.date = dtList[0];
                    }
                    else
                    {
                        dividModel.date = dtList.First(m => m != dtList[0]);
                    }
                    #endregion
                    CalWeight(ref dividModel); //统计各种物料
                    DividList.Add(dividModel);
                    dividModel = new DividBatchhis();
                    dividModel.batList = new List<BATCHHIS>();
                    dividModel.batList.Add(tempList[i]);
                }
            }
            return DividList;
        }
        /// <summary>
        /// 计算班的信息
        /// </summary>
        private void CalClassDayDiffIron()
        {
            DayHearthIronDiff = new List<HEARTHBALANCE_IRONDIFF_DAY>();
            ClassHearthIronDiff = new List<HEARTHBALANCE_IRONDIFF_CLASS>();
            //BatDivList = new List<DividBatchhis>();

            //if (batchhisList.Count > 0)
            //{
            //    BatDivList = dividBatchhis(batchhisList);
            //}


            TapHearthIronDiff.RemoveAll(match => match.TIMESTAMP == TapHearthIronDiff.Min(m => m.TIMESTAMP)); //去掉最早时间的数据，避免班次统计不完整

            foreach (var a in TapHearthIronDiff.GroupBy(m => m.TIMESTAMP))
            {
                //处理天的产量
                HEARTHBALANCE_IRONDIFF_DAY DayModel = new HEARTHBALANCE_IRONDIFF_DAY();
                List<HEARTHBALANCE_IRONDIFF> DayList = a.ToList();
                DayModel.TIMESTAMP = DayList[0].TIMESTAMP;
                if (DayList.FindIndex(m => m.SI > 0) >= 0)
                {
                    DayModel.SI_AVG = DayList.FindAll(m => m.SI > 0).Average(m => m.SI);
                }
                DayModel.IRON_REAL = DayList.Sum(m => m.IRON_REAL);
                DayModel.IRONPRODRATIO = DayList.Average(m => m.IRONPRODRATIO);
                DayModel.SLAGPRODRATIO = DayList.Average(m => m.SLAGPRODRATIO);
                DayModel.IRON_THEORY = DayList.Sum(m => m.IRON_THEORY);
                DayModel.IRONDIFF = DayList.Sum(m => m.IRONDIFF);
                //List<DividBatchhis> ListBatDivd = BatDivList.FindAll(m => m.date == DayModel.TIMESTAMP);
                //if (ListBatDivd.Count > 0)
                //{
                //    DayModel.IRON_THEORY = ListBatDivd.Sum(m => m.Yield); //理论产量
                //    DayModel.YIELD = DayModel.IRONDIFF + DayModel.IRON_THEORY; //体检产量等于铁量差加理论铁量
                //    DayModel.SINTERWEIGHT = ListBatDivd.Sum(m => m.SinterWeight);
                //    DayModel.PELLETWEIGHT = ListBatDivd.Sum(m => m.PelletWeight);
                //    DayModel.CURDEWEIGHT = ListBatDivd.Sum(m => m.CrudeWeight);
                //    DayModel.COKEWEIGHT = ListBatDivd.Sum(m => m.CokeWeight);
                //    DayModel.LITTLEWEIGHT = ListBatDivd.Sum(m => m.LittleWeight);
                //    DayModel.FLUXWEIGHT = ListBatDivd.Sum(m => m.FluxWeight);

                //}
                double SumAbs = 0;
                int count = 0;
                DayList.ForEach(delegate (HEARTHBALANCE_IRONDIFF m)
                {
                    if (m.SI > 0)
                    {
                        SumAbs += Math.Abs(DayModel.SI_AVG.ToDouble() - m.SI);
                        count++;
                    }

                }
                     );
                if (count > 0)
                {
                    DayModel.SI_DIFF = SumAbs / count;
                }
                DayHearthIronDiff.Add(DayModel);




                //处理班的产量
                foreach (var item in a.GroupBy(m => m.BC))
                {
                    HEARTHBALANCE_IRONDIFF_CLASS modelTemp = new HEARTHBALANCE_IRONDIFF_CLASS();
                    List<HEARTHBALANCE_IRONDIFF> tempList = item.ToList();
                    modelTemp.TIMESTAMP = tempList[0].TIMESTAMP;
                    modelTemp.BC = tempList[0].BC;
                    modelTemp.BB = tempList[0].BB;
                    //int index = BatDivList.FindIndex(m => m.date == modelTemp.TIMESTAMP && m.BC == modelTemp.BC);
                    //if (index > 0)
                    //{
                    //    modelTemp.IRON_THEORY = BatDivList[index].Yield;
                    //    modelTemp.SINTERWEIGHT = BatDivList[index].SinterWeight;
                    //    modelTemp.PELLETWEIGHT = BatDivList[index].PelletWeight;
                    //    modelTemp.CURDEWEIGHT = BatDivList[index].CrudeWeight;
                    //    modelTemp.COKEWEIGHT = BatDivList[index].CokeWeight;
                    //    modelTemp.FLUXWEIGHT = BatDivList[index].FluxWeight;
                    //    modelTemp.LITTLEWEIGHT = BatDivList[index].LittleWeight;
                    //}

                    modelTemp.IRON_REAL = tempList.Sum(m => m.IRON_REAL);//统计铁次实际铁量
                    modelTemp.IRONDIFF = tempList.Sum(m => m.IRONDIFF); //铁次铁量差之和
                    modelTemp.IRON_THEORY = tempList.Sum(m => m.IRON_THEORY); //理论铁量之和
                    modelTemp.IRONPRODRATIO = tempList.Average(m => m.IRONPRODRATIO);
                    modelTemp.SLAGPRODRATIO = tempList.Average(m => m.SLAGPRODRATIO);
                    // modelTemp.SI_AVG = tempList.FindAll(m => m.SI > 0).Average(m => m.SI);
                    if (tempList.FindIndex(m => m.SI > 0) >= 0)
                    {
                        modelTemp.SI_AVG = tempList.FindAll(m => m.SI > 0).Average(m => m.SI);
                    }

                    SumAbs = 0;
                    count = 0;
                    tempList.ForEach(delegate (HEARTHBALANCE_IRONDIFF m)
                    {
                        if (m.SI > 0)
                        {
                            SumAbs += Math.Abs(modelTemp.SI_AVG.ToDouble() - m.SI);
                            count++;
                        }

                    }
                    );
                    if (count > 0)
                    {
                        modelTemp.SI_DIFF = SumAbs / count;
                    }

                    ClassHearthIronDiff.Add(modelTemp);

                }
            }
        }
        /// <summary>
        /// 计算铁次的各项重量
        /// </summary>
        /// <param name="model"></param>
        private void CalWeight(ref DividBatchhis model)
        {
            if (model != null && model.batList != null && model.batList.Count > 0)
            {
                foreach (BATCHHIS item in model.batList)
                {
                    foreach (KeyValuePair<string, string> m in batDic)
                    {
                        Type t = typeof(BATCHHIS);
                        int Code = t.GetProperty(m.Key).GetValue(item, null).ToInt();
                        double Weight = t.GetProperty(m.Value).GetValue(item, null).ToDouble();
                        if (Code >= 20 && Code <= 29)
                        {
                            model.FluxWeight += Weight;
                        }
                        else if (Code >= 30 && Code <= 49 && Code != 39)
                        {
                            model.CokeWeight += Weight;
                        }
                        else if (Code >= 50 && Code <= 59)
                        {
                            model.SinterWeight += Weight;
                        }
                        else if (Code >= 60 && Code <= 79)
                        {
                            model.PelletWeight += Weight;
                        }
                        else if (Code >= 80 && Code <= 99)
                        {
                            model.CrudeWeight += Weight;
                        }
                        else if (Code == 39)
                        {
                            model.LittleWeight += Weight;
                        }
                    }
                    model.Yield += item.A_MHTM.ToDouble();
                }
                model.OreWeight = model.CrudeWeight + model.PelletWeight + model.SinterWeight;
            }
        }
        private List<BATCHHIS_TOTAL> CalBatchhisTotal(DateTime strTime)
        {
            List<BATCHHIS_TOTAL> Batchhis_TotalList = new List<BATCHHIS_TOTAL>();
            List<BATCHHIS> TodayBatchhisList = new List<BATCHHIS>();
            DateTime StartTime = strTime.ToString("yyyy-MM-dd").ToDate();
            if (strTime.Hour >= 20 && strTime.Hour < 24)
            {
                StartTime = strTime.ToString("yyyy-MM-dd").ToDate().AddHours(20);
            }
            else
            {
                StartTime = strTime.ToString("yyyy-MM-dd").ToDate().AddDays(-1).AddHours(20);
            }

            DateTime InsertTime = StartTime.AddDays(1).ToString("yyyy-MM-dd").ToDate();
            #region 读取槽下排的料
            if (batchhisList.Count > 0)
            {
                TodayBatchhisList = batchhisList.FindAll(m => m.TIMESTAMP > StartTime && m.TIMESTAMP <= strTime);
                if (TodayBatchhisList.Count > 0)
                {
                    foreach (BATCHHIS item in TodayBatchhisList)
                    {
                        foreach (KeyValuePair<string, string> m in batDic)
                        {
                            Type t = typeof(BATCHHIS);
                            int Code = t.GetProperty(m.Key).GetValue(item, null).ToInt();
                            double Weight = t.GetProperty(m.Value).GetValue(item, null).ToDouble();
                            int index = Batchhis_TotalList.FindIndex(match => match.CODE == Code);
                            if (Code > 0)
                            {
                                //如果集合有，累计，如果没有新增
                                if (index >= 0)
                                {
                                    Batchhis_TotalList[index].WEIGHT += Weight;
                                }
                                else
                                {
                                    Batchhis_TotalList.Add(new BATCHHIS_TOTAL() { TIMESTAMP = InsertTime, CODE = Code, WEIGHT = Weight });
                                }
                            }

                        }
                    }
                }

            }
            #endregion

            #region 处理煤粉
            double Value = 0;
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                Value = idatabase.GetDouble($"select sum(A_WINJ) from AV05M where timestamp>{DataBaseFactory.ConvertTimeString(StartTime, DataBaseType.Oracle)}" +
                    $" and timestamp<={DataBaseFactory.ConvertTimeString(strTime, DataBaseType.Oracle)}");
            }
            Value = Value / 12;
            BATCHHIS_TOTAL tempModel = new BATCHHIS_TOTAL() { TIMESTAMP = InsertTime, CODE = 101, WEIGHT = Value };
            Batchhis_TotalList.Add(tempModel);
            #endregion
            return Batchhis_TotalList;
        }


        public class DividBatchhis
        {
            public DateTime date { get; set; }
            public string BC { get; set; }
            public List<BATCHHIS> batList { get; set; }

            public double Yield { get; set; }
            public double OreWeight { get; set; }
            public double CokeWeight { get; set; }
            public double SinterWeight { get; set; }
            public double PelletWeight { get; set; }
            public double CrudeWeight { get; set; }
            public double FluxWeight { get; set; }

            public double LittleWeight { get; set; }

        }
    }

}
