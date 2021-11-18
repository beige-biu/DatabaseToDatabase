
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BFES.DataAccess;

using System.Data;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using BKYL.Sinter.Plugin.Models;

namespace BKYL.Sinter.Plugin
{
    public class Con_Sf_Ana : BaseJob
    {
        public override void InitTask()
        {
            //加载配置 MC_POPCAL_PAR
        }

        public override void RunTask(DateTime currentTime)
        {
            string log = "";
            try
            {
                //源数据库 表
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
                {
                    // 数据库查询

                }

                //目标数据库 表
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    double MICAL_SAM_MAT_TIME_ = MC_MICAL_RESULT(iDataBase);
                    List<MC_SINCAL_RESULT_1MIN> MC_SINCAL_RESULT_1MIN_ = MC_SINCAL_RESULT_1MIN(MICAL_SAM_MAT_TIME_, currentTime,iDataBase);
                    set_save(MC_SINCAL_RESULT_1MIN_,currentTime);
                }

                Outputlog(log);
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                //释放资源
                // log = null;
            }
        }

        public double MC_MICAL_RESULT(IDataBase iDataBase)
        {
            string str_sql = "select MICAL_SAM_MAT_TIME   from MC_MICAL_RESULT  where DATANUM=14   and  timestamp=(select max(timestamp) from MC_MICAL_RESULT) order by timestamp desc";
            return iDataBase.GetDouble(str_sql);
        }

        public List<MC_SINCAL_RESULT_1MIN> MC_SINCAL_RESULT_1MIN(double MICAL_SAM_MAT_TIME,DateTime currentTime, IDataBase iDataBase)
        {
            DateTime endtime = currentTime.AddMinutes(-MICAL_SAM_MAT_TIME);
            DateTime starttime = endtime.AddMinutes(-6);
            string str_sql = string.Format("select * from MC_SINCAL_RESULT_1MIN where timestamp > to_date('" + starttime + "','yyyy-mm-dd hh24:mi:ss')and timestamp <= to_date('" + endtime + "','yyyy-mm-dd hh24:mi:ss')");
            return iDataBase.GetList<MC_SINCAL_RESULT_1MIN>(str_sql);
        }

        public void set_save(List<MC_SINCAL_RESULT_1MIN> qita,DateTime currentTime)
        {
            MC_SRM_ANA MC_SRM_ANA_ = null;
            MC_SRM_ANA_ = new MC_SRM_ANA();
            double SINCAL_SIN_PV_TFE_=0;
            int a = 0;
            double SINCAL_SIN_PV_FEO_ = 0;
            int b = 0;
            double SINCAL_SIN_PV_CAO_ = 0;
            int c = 0;
            double SINCAL_SIN_PV_SIO2_ = 0;
            int d = 0;
            double SINCAL_SIN_PV_AL2O3_ = 0;
            int a1 = 0;
            double  SINCAL_SIN_PV_MGO_ = 0;
            int b1 = 0;
            double SINCAL_SIN_PV_S_ = 0;
            int c1 = 0;
            double SINCAL_SIN_PV_P_ = 0;
            int d1 = 0;
            double SINCAL_SIN_PV_MN_ = 0;
            int a2 = 0;
            double SINCAL_SIN_PV_R_ = 0;
            int b2 = 0;
            double SINCAL_SIN_PV_TIO2_ = 0;
            int c2 = 0;
            double SINCAL_SIN_PV_K2O_ = 0;
            int d2 = 0;
            double SINCAL_SIN_PV_NA2O_ = 0;
            int a3 = 0;
            double SINCAL_SIN_PV_PBO_ = 0;
            int b3 = 0;
            double SINCAL_SIN_PV_ZNO_ = 0;
            int c3 = 0;
            double SINCAL_SIN_PV_TI_ = 0;
            int d3 = 0;
            double SINCAL_SIN_PV_AS_ = 0;
            int a4 = 0;
            double SINCAL_SIN_PV_CU_ = 0;
            int b4 = 0;
            double SINCAL_SIN_PV_PB_ = 0;
            int c4 = 0;
            double SINCAL_SIN_PV_ZN_ = 0;
            int d4 = 0;
            double SINCAL_SIN_PV_K_ = 0;
            int a5 = 0;
            double SINCAL_SIN_PV_NA_ = 0;
            int b5 = 0;
            double SINCAL_SIN_PV_CR_ = 0;
            int c5 = 0;
            double SINCAL_SIN_PV_NI_ = 0;
            int d5 = 0;
            for (int i=0;i< qita.Count;i++)
            {
              if (qita[i].SINCAL_SIN_PV_TFE>0)
                {
                    SINCAL_SIN_PV_TFE_ += qita[i].SINCAL_SIN_PV_TFE.ToDouble();
                    a += 1;
                }
                if (qita[i].SINCAL_SIN_PV_FEO > 0)
                {
                    SINCAL_SIN_PV_FEO_ += qita[i].SINCAL_SIN_PV_FEO.ToDouble();
                    b += 1;
                }
                if (qita[i].SINCAL_SIN_PV_CAO > 0)
                {
                    SINCAL_SIN_PV_CAO_ += qita[i].SINCAL_SIN_PV_CAO.ToDouble();
                    c += 1;
                }
                if (qita[i].SINCAL_SIN_PV_SIO2 > 0)
                {
                    SINCAL_SIN_PV_SIO2_ += qita[i].SINCAL_SIN_PV_SIO2.ToDouble();
                    d += 1;
                }
                if (qita[i].SINCAL_SIN_PV_AL2O3 > 0)
                {
                    SINCAL_SIN_PV_AL2O3_ += qita[i].SINCAL_SIN_PV_AL2O3.ToDouble();
                    a1 += 1;
                }
                if (qita[i].SINCAL_SIN_PV_MGO > 0)
                {
                    SINCAL_SIN_PV_MGO_ += qita[i].SINCAL_SIN_PV_MGO.ToDouble();
                    b1 += 1;
                }
                if (qita[i].SINCAL_SIN_PV_S > 0)
                {
                    SINCAL_SIN_PV_S_ += qita[i].SINCAL_SIN_PV_S.ToDouble();
                    c1 += 1;
                }
                if (qita[i].SINCAL_SIN_PV_P > 0)
                {
                    SINCAL_SIN_PV_P_ += qita[i].SINCAL_SIN_PV_P.ToDouble();
                    d1 += 1;
                }
                if (qita[i].SINCAL_SIN_PV_MN > 0)
                {
                    SINCAL_SIN_PV_MN_ += qita[i].SINCAL_SIN_PV_MN.ToDouble();
                    a2 += 1;
                }
                if (qita[i].SINCAL_SIN_PV_R > 0)
                {
                    SINCAL_SIN_PV_R_ += qita[i].SINCAL_SIN_PV_R.ToDouble();
                    b2 += 1;
                }
                if (qita[i].SINCAL_SIN_PV_TIO2 > 0)
                {
                    SINCAL_SIN_PV_TIO2_ += qita[i].SINCAL_SIN_PV_TIO2.ToDouble();
                    c2 += 1;
                }
                if (qita[i].SINCAL_SIN_PV_K2O > 0)
                {
                    SINCAL_SIN_PV_K2O_ += qita[i].SINCAL_SIN_PV_K2O.ToDouble();
                    d2 += 1;
                }

                if (qita[i].SINCAL_SIN_PV_NA2O > 0)
                {
                    SINCAL_SIN_PV_NA2O_ += qita[i].SINCAL_SIN_PV_NA2O.ToDouble();
                    a3 += 1;
                }
                if (qita[i].SINCAL_SIN_PV_PBO > 0)
                {
                    SINCAL_SIN_PV_PBO_ += qita[i].SINCAL_SIN_PV_PBO.ToDouble();
                    b3 += 1;
                }
                if (qita[i].SINCAL_SIN_PV_ZNO > 0)
                {
                    SINCAL_SIN_PV_ZNO_ += qita[i].SINCAL_SIN_PV_ZNO.ToDouble();
                    c3 += 1;
                }
                if (qita[i].SINCAL_SIN_PV_TI > 0)
                {
                    SINCAL_SIN_PV_TI_ += qita[i].SINCAL_SIN_PV_TI.ToDouble();
                    d3 += 1;
                }
                if (qita[i].SINCAL_SIN_PV_AS > 0)
                {
                    SINCAL_SIN_PV_AS_ += qita[i].SINCAL_SIN_PV_AS.ToDouble();
                    a4 += 1;
                }
                if (qita[i].SINCAL_SIN_PV_CU > 0)
                {
                    SINCAL_SIN_PV_CU_ += qita[i].SINCAL_SIN_PV_CU.ToDouble();
                    b4 += 1;
                }
                if (qita[i].SINCAL_SIN_PV_PB > 0)
                {
                    SINCAL_SIN_PV_PB_ += qita[i].SINCAL_SIN_PV_PB.ToDouble();
                    c4 += 1;
                }
                if (qita[i].SINCAL_SIN_PV_ZN > 0)
                {
                    SINCAL_SIN_PV_ZN_ += qita[i].SINCAL_SIN_PV_ZN.ToDouble();
                    d4 += 1;
                }
                if (qita[i].SINCAL_SIN_PV_K > 0)
                {
                    SINCAL_SIN_PV_K_ += qita[i].SINCAL_SIN_PV_K.ToDouble();
                    a5 += 1;
                }
                if (qita[i].SINCAL_SIN_PV_NA > 0)
                {
                    SINCAL_SIN_PV_NA_ += qita[i].SINCAL_SIN_PV_NA.ToDouble();
                    b5 += 1;
                }
                if (qita[i].SINCAL_SIN_PV_CR > 0)
                {
                    SINCAL_SIN_PV_CR_ += qita[i].SINCAL_SIN_PV_CR.ToDouble();
                    c5 += 1;
                }
                if (qita[i].SINCAL_SIN_PV_NI > 0)
                {
                    SINCAL_SIN_PV_NI_ += qita[i].SINCAL_SIN_PV_NI.ToDouble();
                    d5 += 1;
                }
            }
            MC_SRM_ANA_.TIMESTAMP = currentTime;
            if (a > 0)
            {
                MC_SRM_ANA_.SINCAL_SIN_SIN_PV_TFE =Math.Round(SINCAL_SIN_PV_TFE_ / a,3);
            }
            if (b > 0)
            {
                MC_SRM_ANA_.SINCAL_SIN_PV_FEO = Math.Round(SINCAL_SIN_PV_FEO_ / b,3);
            }
            if (c>0)
            {
                MC_SRM_ANA_.SINCAL_SIN_PV_CAO = Math.Round(SINCAL_SIN_PV_CAO_ / c,3);
            }
            if (d>0)
            {
                MC_SRM_ANA_.SINCAL_SIN_PV_SIO2 = Math.Round(SINCAL_SIN_PV_SIO2_ / d,3);
            }
            if (a1 > 0)
            {
                MC_SRM_ANA_.SINCAL_SIN_PV_AL2O3 = Math.Round(SINCAL_SIN_PV_AL2O3_ / a1,3);
            }
            if (b1 > 0)
            {
                MC_SRM_ANA_.SINCAL_SIN_PV_MGO = Math.Round(SINCAL_SIN_PV_MGO_ / b1,3);
            }
            if (c1 > 0)
            {
                MC_SRM_ANA_.SINCAL_SIN_PV_S = Math.Round(SINCAL_SIN_PV_S_ / c1,3);
            }
            if (d1 > 0)
            {
                MC_SRM_ANA_.SINCAL_SIN_PV_P = Math.Round(SINCAL_SIN_PV_P_ / d1,3);
            }
            if (a2 > 0)
            {
                MC_SRM_ANA_.SINCAL_SIN_PV_MN = Math.Round(SINCAL_SIN_PV_MN_ / a2,3);
            }
            if (b2 > 0)
            {
                MC_SRM_ANA_.SINCAL_SIN_PV_R = Math.Round(SINCAL_SIN_PV_R_ / b2,3);
            }
            if (c2 > 0)
            {
                MC_SRM_ANA_.SINCAL_SIN_PV_TIO2 = Math.Round(SINCAL_SIN_PV_TIO2_ / c2,3);
            }
            if (d2 > 0)
            {
                MC_SRM_ANA_.SINCAL_SIN_PV_K2O = Math.Round(SINCAL_SIN_PV_K2O_ / d2,3);
            }
            if (a3 > 0)
            {
                MC_SRM_ANA_.SINCAL_SIN_PV_NA2O = Math.Round(SINCAL_SIN_PV_NA2O_ / a3,3);
            }
            if (b3 > 0)
            {
                MC_SRM_ANA_.SINCAL_SIN_PV_PBO = Math.Round(SINCAL_SIN_PV_PBO_ / b3,3);
            }
            if (c3 > 0)
            {
                MC_SRM_ANA_.SINCAL_SIN_PV_ZNO = Math.Round(SINCAL_SIN_PV_ZNO_ / c3,3);
            }
            //if (d3 > 0)
            //{
            //    MC_SRM_ANA_.SINCAL_SIN_PV_F = SINCAL_SIN_PV_TI_ / c3;
            //}
            if (a4 > 0)
            {
                MC_SRM_ANA_.SINCAL_SIN_PV_AS = Math.Round(SINCAL_SIN_PV_AS_ / a4,3);
            }
            if (b4 > 0)
            {
                MC_SRM_ANA_.SINCAL_SIN_PV_CU = Math.Round(SINCAL_SIN_PV_CU_ / b4,3);
            }
            if (c4 > 0)
            {
                MC_SRM_ANA_.SINCAL_SIN_PV_PB = Math.Round(SINCAL_SIN_PV_PB_ / c4,3);
            }
            if (d4 > 0)
            {
                MC_SRM_ANA_.SINCAL_SIN_PV_ZN = Math.Round(SINCAL_SIN_PV_ZN_ / d4,3);
            }
            if (a5 > 0)
            {
                MC_SRM_ANA_.SINCAL_SIN_PV_K = Math.Round(SINCAL_SIN_PV_K_ / a5,3);
            }
            if (b5 > 0)
            {
                MC_SRM_ANA_.SINCAL_SIN_PV_NA = Math.Round(SINCAL_SIN_PV_NA_ / b5,3);
            }
            if (c5 > 0)
            {
                MC_SRM_ANA_.SINCAL_SIN_PV_CR = Math.Round(SINCAL_SIN_PV_CR_ / c5,3);
            }
            if (d5 > 0)
            {
                MC_SRM_ANA_.SINCAL_SIN_PV_NI = Math.Round(SINCAL_SIN_PV_NI_ / d5,3);
            }
            Set_Admin_add(MC_SRM_ANA_);
         
        }

        public bool Set_Admin_add(MC_SRM_ANA model)
        {
            bool listmodel = true;

            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                listmodel = (bool)iDataBase.Insert(model);
                return listmodel;
            }
        }

        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            Writelog(exception.Message + exception.StackTrace);
        }
    }
}
