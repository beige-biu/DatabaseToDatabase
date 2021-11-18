using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BFES.DataAccess;
using BKYL.Sinter.Plugin.Models;
using System.Data;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using System.Timers;

namespace BKYL.Sinter.Plugin
{
    //烧返后台
    public class Burntoreturn : BaseJob 
    {
      
        string str_sql = "";
        string log = "";
        double W_8 = 0;
        double W_UP = 0;
        double W_LOW = 0;
        int SRMCAL_FLAG = 0;
        bool beyond = true;

        List<T_BURN_COLE_PLC_1MIN> list = null;//获取烧结机信息表
        List<MC_SRM_RESULT> list1 = null;//要插入的表
        List<CONFIG_SINCAL_VALUE> list2 = null;//返矿配比调整模型投入按钮
        List<M_MATERIAL_ANA> list3 = null;  //获取仓号
        List<T_MATCH_PLC_1MIN> list4 = null;  //plc  
        List<MC_SRM_PAR> list5 = null;  //参数表
        List<MC_SINCAL_INTERFACE_TRIG_MIN> list6 = null;//
        List<T_PLC_5S> list7 = null;  //
        List<MC_SINCAL_CONFIG> list8 = null;  //烧返配置表
        int state = 0;
        public override void InitTask()
        {
            //加载配置yanhao  
            InitTimer();
        }
        DateTime LastDateTime2 = DateTime.MinValue;
        public override void RunTask(DateTime currentTime)
        {
            DateTime time = DateTime.Now.ToString().ToDate();
            int isLoadTime = 0;
            int shi = currentTime.Hour * 3600;
            int fen = currentTime.Minute * 60;
            int second = currentTime.Second;
            int seconds = shi + fen + second;

            if (seconds%60==0)
            {
                isLoadTime = 60;//120s刷新一次
            }          
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                try
                {
                    str_sql = string.Format(" select  * from   MC_SRM_PAR");
                    list5 = iDataBase.GetList<MC_SRM_PAR>(str_sql); //获取周期时间
                    int cwstate = list5[0].STATE.ToInt();               
                    DateTime maxDate_Result = iDataBase.Queryable<MC_SRM_RESULT>().Max(m => m.TIMESTAMP).ToDate();
                    //(仓位超限调用)                  
                    str_sql = string.Format("select * from M_MATERIAL_ANA  where  l2_code >= 600 and l2_code <= 699  order by l2_code asc");
                    list3 = iDataBase.GetList<M_MATERIAL_ANA>(str_sql);//仓号
                    int canghaon = list3[0].BARNID.ToInt();
                    string TW_CODE = "T_W_" + canghaon + "_5S";
                    DateTime maxDateTW = iDataBase.Queryable<T_PLC_5S>().Max(m => m.TIMESTAMP).ToDate();//获取最大时间maxDateTW
                    str_sql = string.Format("select  *  from T_PLC_5S where TIMESTAMP =to_date('" + maxDateTW + "','yyyy-mm-dd hh24:mi:ss') ");//获取最新的 
                    list7 = iDataBase.GetList<T_PLC_5S>(str_sql);//仓号数据  
                    W_8 = list7[0].T_W_8_5S.ToDouble();
                    W_UP= list5[0].PAR_W_UP.ToDouble();
                    W_LOW = list5[0].PAR_W_LOW.ToDouble();
                    double par_T1 = list5[0].PAR_BILL_T.ToDouble();//运转周期                
                    //new
                    DateTime maxDateplc = iDataBase.Queryable<T_MATCH_PLC_1MIN>().Max(m => m.TIMESTAMP).ToDate();//获取最大时间plc
                    str_sql = string.Format("select * from MC_SINCAL_CONFIG t where t.bin_id='" + canghaon + "'");
                    list8 = iDataBase.GetList<MC_SINCAL_CONFIG>(str_sql);//从烧返配置表获取去信息
                    string xlk1 = "M_SL_" + list8[0].BLANKING_ID.ToInt() + "";
                    if (list8.Count > 1)
                    {
                        string xlk2 = "M_SL_" + list8[1].BLANKING_ID.ToInt() + "";
                        xlk1 = xlk1 + "+" + xlk2;
                    }
                    str_sql = string.Format("select sum(" + xlk1 + ") as ISQY from T_MATCH_PLC_1MIN   where TIMESTAMP =to_date('" + maxDateplc + "','yyyy-mm-dd hh24:mi:ss')");
                    list4 = iDataBase.GetList<T_MATCH_PLC_1MIN>(str_sql);
                    int isqy = list4[0].ISQY.ToInt();                
                    if (isqy > 1 || isqy == 1)
                    {
                        state = 1;
                    }
                    else
                    {
                        state = 0;
                    }
                    //仓位超限调用)
                    if (beyond==true)
                    {                       
                        if (W_8 > W_UP || W_8 == W_UP || W_8 < W_LOW)
                        {   int Count = (list5[0].PAR_BILL_T.ToInt() * 60000).ToInt();
                            IntervalTimer(Count);//获取一个周期的刷新时间 
                            SRMCAL_FLAG = 2;
                            CycleCount();
                            BeginTimer();
                           
                        }
                        //正常周期调用                   
                       
                        if ((LastDateTime2.AddMinutes(par_T1) < DateTime.Now) && beyond == true)
                        {
                            LastDateTime2 = DateTime.Now;                          
                            SRMCAL_FLAG = 1;
                            CycleCount();
                        }
                     
                    }
                       //(目标仓位发生变化)
                        if (cwstate == 1 )
                        {
                            //调用周期
                            SRMCAL_FLAG = 4;
                            CycleCount();
                            LastDateTime2 = DateTime.Now;
                        }

                    //(人工干预返矿配比)
                      if (isLoadTime == 60) {                                         
                        DateTime timenow = DateTime.Now;
                        str_sql = string.Format("select SINCAL_ORE_PV from MC_SINCAL_INTERFACE_TRIG_MIN where sincal_cood = '601'and blanking_id = '8'and (timestamp =(select max(timestamp) from MC_SINCAL_INTERFACE_TRIG_MIN)or  timestamp =(select max(timestamp) from MC_SINCAL_INTERFACE_TRIG_MIN)- interval '1' MINUTE)order by timestamp desc");
                        list6 = iDataBase.GetList<MC_SINCAL_INTERFACE_TRIG_MIN>(str_sql);
                        double npeibi = list6[0].SINCAL_ORE_PV.ToDouble();
                        double opeibi = list6[1].SINCAL_ORE_PV.ToDouble();
                        iDataBase.BeginTran();
                        if (npeibi != opeibi)
                        {
                            str_sql = string.Format("select * from   CONFIG_SINCAL_VALUE");
                            list2 = iDataBase.GetList<CONFIG_SINCAL_VALUE>(str_sql);//查询model状态
                            int modelstate = list2[0].MCR_FLAG.ToInt();
                            str_sql = string.Format(" insert  into  MC_SRM_result (TIMESTAMP,SRMCAL_FLAG,SRMCAL_A_FLAG,SRMCAL_SIG,SRMCAL_W,SRMCAL_BILL_SP,SRMCAL_W_AIM,SRMCAL_W_LAST,SRMCAL_E,SRMCAL_EC,SRMCAL_BILL_SP_A,SRMCAL_BILL_SP_NEW,SRMCAL_BILL_SP_OLD,SRMCAL_RS_FLAG,SRMCAL_SL_FLAG) values (to_date('" + timenow + "', 'yyyy-mm-dd hh24:mi:ss'),'3','1','"+ state + "','0','0','0','0','0','0','0','0','0','2','"+ modelstate + "')");                        
                            iDataBase.ExecuteCommand(str_sql);
                            iDataBase.CommitTran();
                            LastDateTime2 = DateTime.Now;//加长一个周期
                        }
                   }
                  
                }            
                  catch (Exception ex)
                {
                    StopTimer();
                   // iDataBase.RollbackTran();
                    log = string.Format("执行失败。原因：{0}", ex.Message);
                }
                finally
                {
                    Outputlog(log);
                    if (list != null)
                    {
                        list.Clear();
                        list = null;
                    }
                    if (list1 != null)
                    {
                        list1.Clear();
                        list1 = null;
                    }
                    if (list2 != null)
                    {
                        list2.Clear();
                        list2 = null;
                    }
                    if (list3 != null)
                    {
                        list3.Clear();
                        list3 = null;
                    }
                    if (list4 != null)
                    {
                        list4.Clear();
                        list4 = null;
                    }
                    if (list5 != null)
                    {
                        list5.Clear();
                        list5 = null;
                    }
                    if (list6 != null)
                    {
                        list6.Clear();
                        list6 = null;
                    }
                    if (list7 != null)
                    {
                        list7.Clear();
                        list7 = null;
                    }
                    if (list8 != null)
                    {
                        list8.Clear();
                        list8 = null;
                    }
                    
                    log = null;
                }

            }
        }
       
        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            Writelog(exception.Message + exception.StackTrace);
        }

        public override void Dispose()
        {
            DisposeTimer();
            base.Dispose();
        }
        //周期性计算
        public void CycleCount()
        {
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                iDataBase.BeginTran();
                try
                {
                    DateTime maxDate = iDataBase.Queryable<T_BURN_COLE_PLC_1MIN>().Max(m => m.TIMESTAMP).ToDate();//获取最大时间
                    DateTime maxDateplc = iDataBase.Queryable<T_MATCH_PLC_1MIN>().Max(m => m.TIMESTAMP).ToDate();//获取最大时间plc
                    str_sql = string.Format("select * from T_BURN_COLE_PLC_1MIN where timestamp=to_date('" + maxDate + "','yyyy-mm-dd hh24:mi:ss')");
                    list = iDataBase.GetList<T_BURN_COLE_PLC_1MIN>(str_sql);//获设烧结机信息


                    str_sql = string.Format("select * from   CONFIG_SINCAL_VALUE");
                    list2 = iDataBase.GetList<CONFIG_SINCAL_VALUE>(str_sql);//查询model状态

                    //e
                    str_sql = string.Format("select * from M_MATERIAL_ANA  where  l2_code > 600 and l2_code<699  order by l2_code asc");
                    list3 = iDataBase.GetList<M_MATERIAL_ANA>(str_sql);//仓号
                    int canghaon = list3[0].BARNID.ToInt();
                    int code = list3[0].L2_CODE.ToInt();
                    string canhao = "M_W_" + canghaon + "";

                    //检测下料口是否启用
                    //str_sql = string.Format("select * from MC_SINCAL_CONFIG t where t.bin_id='"+ canghaon + "'");
                    //list8 = iDataBase.GetList<MC_SINCAL_CONFIG>(str_sql);//从烧返配置表获取去信息
                    //string xlk1 = "M_SL_" + list8[0].BLANKING_ID.ToInt() + "";                   
                    //if (list8.Count > 1)
                    //{
                    //    string xlk2 = "M_SL_" + list8[1].BLANKING_ID.ToInt() + "";
                    //    xlk1 = xlk1 +"+"+ xlk2;
                    //}
                    //str_sql = string.Format("select sum("+ xlk1 + ") as ISQY from T_MATCH_PLC_1MIN   where TIMESTAMP =to_date('"+ maxDateplc + "','yyyy-mm-dd hh24:mi:ss')");             
                    //list4 = iDataBase.GetList<T_MATCH_PLC_1MIN>(str_sql);
                    //int isqy = list4[0].ISQY.ToInt();
                    //int state = 0;
                    //if (isqy > 1 || isqy == 1)
                    //{                    
                    //     state = 1;
                    //}
                    //else{
                    //     state = 0;
                    //}

                    str_sql = string.Format(" select SINCAL_ORE_SP from MC_SINCAL_INTERFACE_TRIG_MIN t where t.sincal_cood = '" + code + "' and timestamp = (select max(timestamp) from MC_SINCAL_INTERFACE_TRIG_MIN )  order by  BLANKING_ID");
                                            
                    list6 = iDataBase.GetList<MC_SINCAL_INTERFACE_TRIG_MIN>(str_sql);//从下料口取料
                    double Bill = list6[0].SINCAL_ORE_SP.ToDouble();

                 
                    str_sql = string.Format("select " + canhao + " from T_MATCH_PLC_1MIN where TIMESTAMP =to_date('" + maxDateplc + "','yyyy-mm-dd hh24:mi:ss') ");//获取8号仓位里烧返数据 
                    list4 = iDataBase.GetList<T_MATCH_PLC_1MIN>(str_sql);//仓号数据
                    double W_8_NEW = list4[0].M_W_8.ToDouble();

                    str_sql = string.Format("select * from   MC_SRM_PAR");
                    list5 = iDataBase.GetList<MC_SRM_PAR>(str_sql);//获取参数表信息
                    double W_AIM = list5[0].PAR_W_AIM.ToDouble();
                    int EC_T = list5[0].PAR_EC_T.ToInt();//倒退时间
                    double W_UP = list5[0].PAR_W_UP.ToDouble();
                    double W_LOW = list5[0].PAR_W_LOW.ToDouble();
                    int cwstate = list5[0].STATE.ToInt();

                    double e = (W_8_NEW - W_AIM).ToDouble();
                    e= Math.Round(e, 3);
                    //求ec
                    DateTime beforetime = maxDateplc.AddMinutes(-EC_T);                  
                    str_sql = string.Format(" select *  from T_MATCH_PLC_1MIN where TIMESTAMP =（select max(timestamp) from T_MATCH_PLC_1MIN where timestamp <= to_date('" + beforetime + "', 'yyyy-mm-dd hh24:mi:ss'))");//获取8号仓位里烧返数据 
                    list4 = iDataBase.GetList<T_MATCH_PLC_1MIN>(str_sql);//仓号数据
                    double W_8_LAST = list4[0].M_W_8.ToDouble();
                    double ec = (W_8_NEW - W_8_LAST).ToDouble();
                    ec = Math.Round(ec, 3);
                    //计算W
                    double W = 0;
                    double e1 = list5[0].PAR_W_E_LEVEL_1.ToDouble();
                    double e2 = list5[0].PAR_W_E_LEVEL_2.ToDouble();
                    double e3 = list5[0].PAR_W_E_LEVEL_3.ToDouble();
                    double e4 = list5[0].PAR_W_E_LEVEL_4.ToDouble();

                    double W1 = list5[0].PAR_BILL_LOW_LOW.ToDouble();
                    double W2 = list5[0].PAR_BILL_LOW.ToDouble();
                    double W3 = list5[0].PAR_BILL_UP.ToDouble();
                    double W4 = list5[0].PAR_BILL_UP_UP.ToDouble();
                    double X = list5[0].PAR_K.ToDouble();

                    if (e > -e1 && e < e1)
                    {
                        W = 0;
                    }
                    else if (ec <= 0 && e > 0)
                    {
                        if (e > e1 && e < e2)
                        {
                            W = W1 - X;
                        }
                        if (e > e2 && e < e3)
                        {
                            W = W2 - X;
                        }
                        if (e > e3 && e < e4)
                        {
                            W = W3 - X;
                        }
                        if (e > e4 && e4 > 0)
                        {
                            W = W4 - X;
                        }

                    }
                    else if (ec > 0 && e > 0)
                    {
                        if (e > e1 && e < e2)
                        {
                            W = W1;
                        }
                        if (e > e2 && e < e3)
                        {
                            W = W2;
                        }
                        if (e > e3 && e < e4)
                        {
                            W = W3;
                        }
                        if (e > e4 && e4 > 0)
                        {
                            W = W4;
                        }
                    }
                    else if (ec >=0 && e < 0)
                    {

                        if (e > -e2 && e < -e1)
                        {
                            W = -W1 + X;
                        }
                        if (e > -e3 && e < -e2)
                        {
                            W = -W2 + X;
                        }
                        if (e > -e4 && e < -e3)
                        {
                            W = -W3 + X;
                        }
                        if (e < -e4)
                        {
                            W = -W4 + X;
                        }
                    }
                    else if (ec < 0 && e < 0)
                    {
                        if (e > -e2 && e < -e1)
                        {
                            W = -W1;
                        }
                        if (e > -e3 && e < -e2)
                        {
                            W = -W2;
                        }
                        if (e > -e4 && e < -e3)
                        {
                            W = -W3;
                        }
                        if (e < -e4)
                        {
                            W = -W4;
                        }
                    }
                    //计算Bill_new                                 
                    if (W_8 > W_UP || W_8 == W_UP)
                    {

                        W = W4;
                    }
                    if (W_8 < W_LOW || W_8 == W_LOW)
                    {

                        W = -W4;
                    }                 
                       double Bill_new = Bill + W;
                    List<MC_SRM_RESULT> list1 = new List<MC_SRM_RESULT>();
                    //(目标仓位发生变化)
                    if (cwstate == 1)
                    {
                        //调用周期
                        str_sql = string.Format(" update  MC_SRM_PAR set  state ='0'");
                        iDataBase.ExecuteCommand(str_sql);
                    }
                    DateTime timenow = System.DateTime.Now;             
                    double speed = list[0].BC_SIN_MS_PV.ToDouble();//获取烧结机实际机速
                    int modelstate = list2[0].MCR_FLAG.ToInt();
                    if (state == 1 && speed > 0.5 && modelstate == 1)//既存库又下发
                    {
                        str_sql = string.Format(" insert  into  MC_SRM_result (TIMESTAMP,SRMCAL_FLAG,SRMCAL_A_FLAG,SRMCAL_SIG,SRMCAL_W,SRMCAL_BILL_SP,SRMCAL_W_AIM,SRMCAL_W_LAST,SRMCAL_E,SRMCAL_EC,SRMCAL_BILL_SP_A,SRMCAL_BILL_SP_NEW,SRMCAL_BILL_SP_OLD,SRMCAL_RS_FLAG,SRMCAL_SL_FLAG) values (to_date('" + timenow + "', 'yyyy-mm-dd hh24:mi:ss'),'"+ SRMCAL_FLAG + "','1','" + state + "','" + W_8_NEW + "','" + Bill + "','" + W_AIM + "','" + W_8_LAST + "','" + e + "','" + ec + "','" + W + "','" + Bill_new + "','" + Bill + "','1','" + modelstate + "')");                  
                        iDataBase.ExecuteCommand(str_sql);
                    }

                    if (state == 1 && speed > 0.5 && modelstate == 0)//只存库不下发
                    {
                        str_sql = string.Format(" insert  into  MC_SRM_result (TIMESTAMP,SRMCAL_FLAG,SRMCAL_A_FLAG,SRMCAL_SIG,SRMCAL_W,SRMCAL_BILL_SP,SRMCAL_W_AIM,SRMCAL_W_LAST,SRMCAL_E,SRMCAL_EC,SRMCAL_BILL_SP_A,SRMCAL_BILL_SP_NEW,SRMCAL_BILL_SP_OLD,SRMCAL_RS_FLAG,SRMCAL_SL_FLAG) values (to_date('" + timenow + "', 'yyyy-mm-dd hh24:mi:ss'),'" + SRMCAL_FLAG + "','1','" + state + "','" + W_8_NEW + "','" + Bill + "','" + W_AIM + "','" + W_8_LAST + "','" + e + "','" + ec + "','" + W + "','" + Bill_new + "','" + Bill + "','2','" + modelstate + "')");                     
                        iDataBase.ExecuteCommand(str_sql);
                    }

                    if (state == 0 || speed < 0.5)//不库存(停止计算),不下发
                    {
                        
                        str_sql = string.Format(" insert  into  MC_SRM_result (TIMESTAMP,SRMCAL_FLAG,SRMCAL_A_FLAG,SRMCAL_SIG,SRMCAL_W,SRMCAL_BILL_SP,SRMCAL_W_AIM,SRMCAL_W_LAST,SRMCAL_E,SRMCAL_EC,SRMCAL_BILL_SP_A,SRMCAL_BILL_SP_NEW,SRMCAL_BILL_SP_OLD,SRMCAL_RS_FLAG,SRMCAL_SL_FLAG) values (to_date('" + timenow + "', 'yyyy-mm-dd hh24:mi:ss'),'0','0','0','0','0','0','0','0','0','0','0','0','3','0')");                      
                        iDataBase.ExecuteCommand(str_sql);
                    }
                    iDataBase.CommitTran();
                }
                catch (Exception ex)
                {
                    iDataBase.RollbackTran();
                    log = string.Format("执行失败。原因：{0}", ex.Message);
                }
                finally
                {
                    Outputlog(log);
                    if (list != null)
                    {
                        list.Clear();
                        list = null;
                    }
                    if (list1 != null)
                    {
                        list1.Clear();
                        list1 = null;
                    }
                    if (list2 != null)
                    {
                        list2.Clear();
                        list2 = null;
                    }
                    if (list3 != null)
                    {
                        list3.Clear();
                        list3 = null;
                    }
                    if (list4 != null)
                    {
                        list4.Clear();
                        list4 = null;
                    }
                    if (list5 != null)
                    {
                        list5.Clear();
                        list5 = null;
                    }
                    if (list6 != null)
                    {
                        list6.Clear();
                        list6 = null;
                    }
                    if (list7 != null)
                    {
                        list7.Clear();
                        list7 = null;
                    }
                    if (list8 != null)
                    {
                        list8.Clear();
                        list8 = null;
                    }
                    log = null;
                }

            }

        }
        
        #region 时间
      
        System.Timers.Timer t_time = null;

        void InitTimer()
        {
            if (t_time == null)
            {
                t_time = new System.Timers.Timer(60000);
            }
        }
        void IntervalTimer(int num)
        {
            if (t_time != null)
            {
                t_time.Interval = num;
                t_time.Elapsed += T_time_Elapsed;
            }
        }

        void BeginTimer()
        {
            //
            beyond = false;
            if (t_time != null)
            {
                t_time.Enabled = true;
                t_time.Start();
            }
        }
        void StopTimer()
        {
            if (t_time != null)
            {
                t_time.Enabled = false;
                t_time.Stop();
            }
            //
            beyond = true;
        }
        void DisposeTimer()
        {
            StopTimer();
            if (t_time != null)
            {
                t_time.Dispose();
                t_time = null;
            }
        }

        private void T_time_Elapsed(object sender, ElapsedEventArgs e)
        {
            //
            StopTimer();       
            beyond = true;
        }
        #endregion


    }
}
