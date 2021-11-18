using BFES.DataAccess;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using BKYL.QT.Plugin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.QT.Plugin.Job
{
    
    public class Job_Update : BaseJob
    {
        // 原表
        MATERIAL_STORAGE_BIN MATERIAL_STORAGE_BIN_ = null;

        public override void InitTask()        {
        }

        public override void RunTask(DateTime currentTime)
        {
            string log = "";
            try
            {
                //源数据库 表
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
                {
                    MATERIAL_STORAGE_BIN_= GetMATERIAL_STORAGE_BIN(iDataBase);

                }
                //目标数据库 表
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {

                    if (MATERIAL_STORAGE_BIN_ != null)
                    {
                        Update_TC_MATERIALR_BIN(iDataBase, MATERIAL_STORAGE_BIN_);
                    }
                }

                Outputlog(log);
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                if (MATERIAL_STORAGE_BIN_ != null)
                {
                    MATERIAL_STORAGE_BIN_ = null;
                }
            }
        }
        public  MATERIAL_STORAGE_BIN GetMATERIAL_STORAGE_BIN(IDataBase iDataBase)
        {
            string sqlStr = string.Format("select * from MATERIAL_STORAGE_BIN");
            return iDataBase.GetSingle<MATERIAL_STORAGE_BIN>(sqlStr);
        }

        public void Update_TC_MATERIALR_BIN(IDataBase iDataBase, MATERIAL_STORAGE_BIN MATERIAL_STORAGE_BIN_)
        {
            TC_MATERIALR_BIN TC_MATERIALR_BIN_ = null;
            TC_MATERIALR_BIN_ = new TC_MATERIALR_BIN();
            TC_MATERIALR_BIN_.TIMESTAMP = MATERIAL_STORAGE_BIN_.TIMESTAMP.ToDate();
            TC_MATERIALR_BIN_.M_CODE_1 = MATERIAL_STORAGE_BIN_.YPLLC_COOD1;
            TC_MATERIALR_BIN_.M_CODE_2 = MATERIAL_STORAGE_BIN_.YPLLC_COOD2;
            TC_MATERIALR_BIN_.M_CODE_3 = MATERIAL_STORAGE_BIN_.YPLLC_COOD3;
            TC_MATERIALR_BIN_.M_CODE_4 = MATERIAL_STORAGE_BIN_.YPLLC_COOD4;
            TC_MATERIALR_BIN_.M_CODE_5 = MATERIAL_STORAGE_BIN_.YPLLC_COOD5;
            TC_MATERIALR_BIN_.M_CODE_6 = MATERIAL_STORAGE_BIN_.YPLLC_COOD6;
            TC_MATERIALR_BIN_.M_CODE_7 = MATERIAL_STORAGE_BIN_.YPLLC_COOD7;
            TC_MATERIALR_BIN_.M_CODE_8 = MATERIAL_STORAGE_BIN_.PLJPC_COOD1;
            TC_MATERIALR_BIN_.M_CODE_9 = MATERIAL_STORAGE_BIN_.PLJPC_COOD2;
            TC_MATERIALR_BIN_.M_CODE_10 = MATERIAL_STORAGE_BIN_.PLJPC_COOD3;
            TC_MATERIALR_BIN_.M_CODE_11 = MATERIAL_STORAGE_BIN_.PLJPC_COOD4;
            TC_MATERIALR_BIN_.M_CODE_12 = MATERIAL_STORAGE_BIN_.RJLC_COOD1;
            TC_MATERIALR_BIN_.M_CODE_13 = MATERIAL_STORAGE_BIN_.RJLC_COOD2;
            TC_MATERIALR_BIN_.M_CODE_14 = MATERIAL_STORAGE_BIN_.ZTLC_COOD1;
            TC_MATERIALR_BIN_.M_CODE_15 = MATERIAL_STORAGE_BIN_.ZTLC_COOD2;
            TC_MATERIALR_BIN_.M_CODE_16 = MATERIAL_STORAGE_BIN_.CCHLC_COOD1;
            TC_MATERIALR_BIN_.M_CODE_17 = MATERIAL_STORAGE_BIN_.CCHLC_COOD2;

            if (Set_update(TC_MATERIALR_BIN_))
            {
                Log.WriteLine("原料种类更新完成！");
               
            }
            else
            {
                Log.WriteLine ("原料种类更新失败！");
            }
        }


        public bool Set_update(TC_MATERIALR_BIN TC_MATERIALR_BIN_)
        {
            string log = "";
            bool listmodel = false;
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    iDataBase.BeginTran();
                    string sql = "";
                    sql = string.Format(@"update TC_MATERIALR_BIN set M_CODE_1 = '" + TC_MATERIALR_BIN_.M_CODE_1 + "'"
                    + ",M_CODE_2= '" + TC_MATERIALR_BIN_.M_CODE_2 + "' "
                    + ",M_CODE_3= '" + TC_MATERIALR_BIN_.M_CODE_3+ "'"
                    + ",M_CODE_4= '" + TC_MATERIALR_BIN_.M_CODE_4 + "'"
                    + ",M_CODE_5= '" + TC_MATERIALR_BIN_.M_CODE_5 + "'"
                    + ",M_CODE_6= '" + TC_MATERIALR_BIN_.M_CODE_6 + "'"
                    + ",M_CODE_7= '" + TC_MATERIALR_BIN_.M_CODE_7 + "'"
                    + ",M_CODE_8= '" + TC_MATERIALR_BIN_.M_CODE_8 + "'"
                    + ",M_CODE_9= '" + TC_MATERIALR_BIN_.M_CODE_9 + "'"
                    + ",M_CODE_10= '" + TC_MATERIALR_BIN_.M_CODE_10 + "'"
                    + ",M_CODE_11= '" + TC_MATERIALR_BIN_.M_CODE_11 + "'"
                    + ",M_CODE_12= '" + TC_MATERIALR_BIN_.M_CODE_12 + "'"
                    + ",M_CODE_13= '" + TC_MATERIALR_BIN_.M_CODE_13 + "'"
                    + ",M_CODE_14= '" + TC_MATERIALR_BIN_.M_CODE_14 + "'"
                    + ",M_CODE_15= '" + TC_MATERIALR_BIN_.M_CODE_15 + "'"
                    + ",M_CODE_16= '" + TC_MATERIALR_BIN_.M_CODE_16 + "'"
                    + ",M_CODE_17= '" + TC_MATERIALR_BIN_.M_CODE_17 + "' ");
                    iDataBase.ExecuteCommand(string.Format(sql));
                    iDataBase.CommitTran();
                    log = string.Format("更新料种完成！");
                    Outputlog(log);
                    listmodel = true;
                    return listmodel;
                }
            }
            catch
            {
                return listmodel;
            }
        }

    }
}
