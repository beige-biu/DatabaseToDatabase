
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
    public class Con_SJZL : BaseJob
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
                    // 数据库查询
                    MC_SRM_RESULT_1MIN MC_SRM_RESULT_1MIN_ = Get_MC_SRM_RESULT_1MIN(iDataBase);
                    MC_SINCAL_RESULT_1MIN MC_SINCAL_RESULT_1MIN_= Get_MC_SINCAL_RESULT_1MIN(iDataBase);
                    MC_SINCAL_INTERFACE_VA MC_SINCAL_INTERFACE_VA_=Get_MC_SINCAL_INTERFACE_VA(iDataBase);
                    M_FPQCAL_PAR M_FPQCAL_PAR_=Get_M_FPQCAL_PAR(iDataBase);
                    set_save(MC_SRM_RESULT_1MIN_, MC_SINCAL_RESULT_1MIN_, MC_SINCAL_INTERFACE_VA_, M_FPQCAL_PAR_, currentTime);
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

        public MC_SRM_RESULT_1MIN Get_MC_SRM_RESULT_1MIN(IDataBase iDataBase)
        {
            string str_sql = string.Format(" select  * from  MC_SRM_RESULT_1MIN  where TIMESTAMP > = (select max(TIMESTAMP) from MC_SRM_RESULT_1MIN)");
            return iDataBase.GetSingle<MC_SRM_RESULT_1MIN>(str_sql);
        }

        public MC_SINCAL_RESULT_1MIN Get_MC_SINCAL_RESULT_1MIN(IDataBase iDataBase)
        {
            string str_sql = string.Format(" select  * from  MC_SINCAL_RESULT_1MIN  where TIMESTAMP > = (select max(TIMESTAMP) from MC_SINCAL_RESULT_1MIN)");
            return iDataBase.GetSingle<MC_SINCAL_RESULT_1MIN>(str_sql);
        }

        public MC_SINCAL_INTERFACE_VA Get_MC_SINCAL_INTERFACE_VA(IDataBase iDataBase)
        {
            string str_sql = string.Format(" select  * from  MC_SINCAL_INTERFACE_VA  where TIMESTAMP > = (select max(TIMESTAMP) from MC_SINCAL_INTERFACE_VA)");
            return iDataBase.GetSingle<MC_SINCAL_INTERFACE_VA>(str_sql);
        }

        public M_FPQCAL_PAR Get_M_FPQCAL_PAR(IDataBase iDataBase)
        {
            string str_sql = string.Format(" select  * from  M_FPQCAL_PAR  where TIMESTAMP > = (select max(TIMESTAMP) from M_FPQCAL_PAR)");
            return iDataBase.GetSingle<M_FPQCAL_PAR>(str_sql);
        }

        public void set_save(MC_SRM_RESULT_1MIN MC_SRM_RESULT_1MIN_, MC_SINCAL_RESULT_1MIN MC_SINCAL_RESULT_1MIN_, MC_SINCAL_INTERFACE_VA MC_SINCAL_INTERFACE_VA_, M_FPQCAL_PAR M_FPQCAL_PAR_, DateTime currentTime)
        {
            MC_SINCAL_C_TRIG_CURVE MC_SINCAL_C_TRIG_CURVE_ = null;
            MC_SINCAL_C_TRIG_CURVE_ = new MC_SINCAL_C_TRIG_CURVE();
            MC_SINCAL_C_TRIG_CURVE_.TIMESTAMP = currentTime;
            MC_SINCAL_C_TRIG_CURVE_.SINCAL_FUEL_RATIO = MC_SRM_RESULT_1MIN_.SRMCAL_FUEL_BILL_SP;
            MC_SINCAL_C_TRIG_CURVE_.SINCAL_BRUN_RATIO = MC_SRM_RESULT_1MIN_.SRMCAL_BILL_SP;
            MC_SINCAL_C_TRIG_CURVE_.SINCAL_MIX_SP_LOT = MC_SINCAL_RESULT_1MIN_.SINCAL_MIX_PV_LOT;
            MC_SINCAL_C_TRIG_CURVE_.SINCAL_NON_FUEL_SP_C = MC_SINCAL_RESULT_1MIN_.SINCAL_NON_FUEL_PV_C;
            MC_SINCAL_C_TRIG_CURVE_.SINCAL_R_AIM = MC_SINCAL_INTERFACE_VA_.SINCAL_R_A;
            MC_SINCAL_C_TRIG_CURVE_.SINCAL_R_PV = MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_PV_R;
            MC_SINCAL_C_TRIG_CURVE_.SINCAL_FEO_AIM = M_FPQCAL_PAR_.PAR_FE_CENTER;
            MC_SINCAL_C_TRIG_CURVE_.SINCAL_C_AIM = MC_SINCAL_INTERFACE_VA_.SINCAL_C_A;
            MC_SINCAL_C_TRIG_CURVE_.SINCAL_C_PV = MC_SINCAL_RESULT_1MIN_.SINCAL_MIX_PV_C;
            Set_Admin_add(MC_SINCAL_C_TRIG_CURVE_);

        }

        public bool Set_Admin_add(MC_SINCAL_C_TRIG_CURVE model)
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
