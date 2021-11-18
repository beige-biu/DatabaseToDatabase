using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BFES.DataAccess;
using System.Data;
using BKYL.Sinter.Plugin.Models;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;

namespace BKYL.Sinter.Plugin
{
    //生产组织固体燃耗曲线
    public class SolidConsum : BaseJob
    {
        public override void InitTask()
        {
            //加载配置yanhao
        }

        public override void RunTask(DateTime currentTime)
        {
            DateTime time = DateTime.Now.ToString().ToDate();
            string str_sql = null;
            string log = "";

           
            //检测M
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                List<MC_POPCAL_PAR> list =new List<MC_POPCAL_PAR>();
                T_BURN_COLE_PLC_1MIN list1 = new T_BURN_COLE_PLC_1MIN();
                MC_MICAL_RESULT list2 = null;
                List<MC_POPCAL_CONSUME_1MIN> list4 = null;
                List<MC_POPCAL_CONSUME_1MIN> list5 = null;
                double SINCAL_OUTPUT_PV_ = 0;
                //检测到数据  开始计算公式
                try
                {
                    list = MC_POPCAL_PAR_(iDataBase);
                    double k1 = list[0].PAR_K1.ToDouble();
                    double k2 = list[0].PAR_K2.ToDouble();
                    double a = 0;
                    double H_COKE_ACCU=0;
                    double H_COKE_ACC1=0;
                    double H_TSC_CON = 0;
                    double H_TSC_CON_L = 0;
                    double H_TSC_CON_D = 0;
                    double H_TSC_CON_L_D = 0;
                    double M_HO2 = GET_MD_PHY_PARTICLE_INFO_IN(iDataBase);//     ///读取水分
                    int M_Hour = currentTime.Hour.ToInt();
                    int M_Min = currentTime.Minute.ToInt();
                    if ((M_Hour ==0 || M_Hour == 4 || M_Hour == 8 || M_Hour == 12 || M_Hour == 16 || M_Hour == 20) && M_Min == 0)//
                    {
                        //理论(固体然后理论值
                        //周期内焦炭配料累积值 
                        MC_POPCAL_RESULT_HOUR MC_POPCAL_RESULT_HOUR_ = null;
                        MC_POPCAL_RESULT_HOUR_ = new MC_POPCAL_RESULT_HOUR();
                        DateTime strart = currentTime.AddHours(-4);
                        list4 = get_MC_POPCAL_CONSUME_1MIN(iDataBase, strart, currentTime);
                        SINCAL_OUTPUT_PV_ = GET_MC_SINCAL_RESULT_1MIN(iDataBase, strart, currentTime);//固体理论产量用的
                        H_COKE_ACCU = list4[0].VALUE.ToDouble();//获取被除数焦炭
                        //理论固体燃耗湿基计算
                        if (SINCAL_OUTPUT_PV_ > 0)
                        {
                            H_TSC_CON_L = k1 * H_COKE_ACCU / SINCAL_OUTPUT_PV_ * k2; //固体然后理论值湿基计算公式
                        }
                        if (H_TSC_CON_L > 65)
                        {
                            H_TSC_CON_L = 65;
                        }
                        else if (H_TSC_CON_L < 0)
                        {
                            H_TSC_CON_L = 0;
                        }
                        //理论固体燃耗干基计算
                        if (SINCAL_OUTPUT_PV_ > 0)
                        {
                            H_TSC_CON_L_D = k1 * H_COKE_ACCU * (1 - M_HO2 / 100) / SINCAL_OUTPUT_PV_ * k2; //固体然后理论值湿基计算公式
                        }
                        if (H_TSC_CON_L_D > 65)
                        {
                            H_TSC_CON_L_D = 65;
                        }
                        else if (H_TSC_CON_L_D < 0)
                        {
                            H_TSC_CON_L_D = 0;
                        }
                        MC_POPCAL_RESULT_HOUR_.TIMESTAMP = currentTime;
                        MC_POPCAL_RESULT_HOUR_.POPCAL_H_TSC_CON_LL = H_TSC_CON_L;
                        MC_POPCAL_RESULT_HOUR_.POPCAL_H_TSC_CON_LL_DRY = H_TSC_CON_L_D;
                        MC_POPCAL_RESULT_HOUR_.POPCAL_H_COKE_ACC1 = list4[0].VALUE;
                        MC_POPCAL_RESULT_HOUR_.POPCAL_H_LL_ACCU = SINCAL_OUTPUT_PV_;
                        //保存
                        SAV_MC_POPCAL_RESULT_HOUR(MC_POPCAL_RESULT_HOUR_);
                    }
                    ///计算实际值
                    if ((M_Hour == 8 || M_Hour == 20) && M_Min == 0)
                    {
                        MC_POPCAL_RESULT_12HOUR MC_POPCAL_RESULT_12HOUR_ = null;
                        MC_POPCAL_RESULT_12HOUR_ = new MC_POPCAL_RESULT_12HOUR();
                        ////实际值计算用产量
                        DateTime strart = currentTime.AddHours(-12);
                        list1 = GET_T_BURN_COLE_PLC_1MIN(iDataBase, strart, currentTime);
                        ///周期内焦炭配料累积值                                                      
                        list5 = get_MC_POPCAL_CONSUME_1MIN(iDataBase, strart, currentTime);
                        H_COKE_ACC1 = list5[0].VALUE.ToDouble();//获取被除数
                        if (list1.SPARE32 > 0)
                        {
                            H_TSC_CON = k1 * H_COKE_ACC1 / list1.SPARE32; //实际固体燃耗湿基计算公式
                        }

                        if (H_TSC_CON > 65)
                        {
                            H_TSC_CON = 65;
                        }
                        else if (H_TSC_CON < 0)
                        {
                            H_TSC_CON = 0;
                        }
                        ///实际固体燃耗干基

                        if (list1.SPARE32 > 0)
                        {
                            H_TSC_CON_D = k1 * H_COKE_ACC1 * (1 - M_HO2 / 100) / list1.SPARE32; 
                        }
                        if (H_TSC_CON_D > 65)
                        {
                            H_TSC_CON_D = 65;
                        }
                        else if (H_TSC_CON_D < 0)
                        {
                            H_TSC_CON_D = 0;
                        }
                        MC_POPCAL_RESULT_12HOUR_.TIMESTAMP = currentTime;
                        MC_POPCAL_RESULT_12HOUR_.POPCAL_H_TSC_CON = H_TSC_CON;
                        MC_POPCAL_RESULT_12HOUR_.POPCAL_H_TSC_CON_DRY = H_TSC_CON_D;
                        MC_POPCAL_RESULT_12HOUR_.POPCAL_H_COKE_ACCU = list5[0].VALUE;
                        MC_POPCAL_RESULT_12HOUR_.POPCAL_H_FP_ACCU = list1.SPARE32;
                        SAV_MC_POPCAL_RESULT_12HOUR(MC_POPCAL_RESULT_12HOUR_);
                    }
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

                        list1 = null;
                    }
                    if (list2 != null)
                    {

                        list2 = null;
                    }

                    if (list4 != null)
                    {
                        list4.Clear();
                        list4 = null;
                    }
                    log = null;
                }

            }
        }
        public T_BURN_COLE_PLC_1MIN GET_T_BURN_COLE_PLC_1MIN(IDataBase iDataBase,DateTime timebefore, DateTime currentTime)
        {
            ///计算实际 周期内成品矿重量累积值
            string str_sql = string.Format(" select  NVL(sum (SPARE32),0) as SPARE32 from  T_BURN_COLE_PLC_1MIN  where SPARE32>0 and  TIMESTAMP > = to_date('" + timebefore + "','yyyy-mm-dd hh24:mi:ss') and TIMESTAMP <= to_date('" + currentTime + "','yyyy-mm-dd hh24:mi:ss')");
            return iDataBase.GetSingle<T_BURN_COLE_PLC_1MIN>(str_sql);
        }
        public List<MC_POPCAL_CONSUME_1MIN> get_MC_POPCAL_CONSUME_1MIN(IDataBase iDataBase,DateTime timebefore1, DateTime currentTime)
        {
            string str_sql = string.Format("select NVL(sum(value),0) as value from MC_POPCAL_CONSUME_1MIN t where  code  like '3%' and TIMESTAMP >=to_date('" + timebefore1 + "','yyyy-mm-dd hh24:mi:ss') and TIMESTAMP <= to_date('" + currentTime + "','yyyy-mm-dd hh24:mi:ss')");
            return iDataBase.GetList<MC_POPCAL_CONSUME_1MIN>(str_sql);
        }
        public List<MC_POPCAL_PAR> MC_POPCAL_PAR_(IDataBase iDataBase)
        {
            string str_sql = string.Format("  select  * from   MC_POPCAL_PAR");
            return iDataBase.GetList<MC_POPCAL_PAR>(str_sql); //获取周期时间
        }

        public double GET_MC_SINCAL_RESULT_1MIN(IDataBase iDataBase,DateTime startTime, DateTime endTime)
        {
            string str_sql = string.Format("select round(sum(SINCAL_OUTPUT_PV)) from MC_SINCAL_RESULT_1MIN where   timestamp > to_date('" + startTime + "','yyyy-mm-dd hh24:mi:ss')and timestamp <= to_date('" + endTime + "','yyyy-mm-dd hh24:mi:ss')");
            return iDataBase.GetDouble(str_sql);
        }

        public double GET_MD_PHY_PARTICLE_INFO_IN(IDataBase iDataBase)
        {
            string str_sql = string.Format("select H2O from MD_PHY_PARTICLE_INFO_IN where  timestamp=(select max(timestamp) from MD_PHY_PARTICLE_INFO_IN where L2_CODE >=301 and  L2_CODE <=399)");
            return iDataBase.GetDouble(str_sql);
        }

        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            Writelog(exception.Message + exception.StackTrace);
        }

        public bool SAV_MC_POPCAL_RESULT_HOUR(MC_POPCAL_RESULT_HOUR model)
        {
            bool listmodel = true;

            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                listmodel = (bool)iDataBase.Insert(model);
                return listmodel;
            }
        }
        public bool SAV_MC_POPCAL_RESULT_12HOUR(MC_POPCAL_RESULT_12HOUR model)
        {
            bool listmodel = true;

            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                listmodel = (bool)iDataBase.Insert(model);
                return listmodel;
            }
        }
    }
}
