
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
    public class Console_l2_lode : BaseJob
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
                List<T_MATERIAL_METERING_MAP> T_MATERIAL_METERING_MAP_ = null;
                //源数据库 表
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
                {
                   T_MATERIAL_METERING_MAP_ = get_T_MATERIAL_METERING_MAP();

                }

                //目标数据库 表
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    GET_MAT(T_MATERIAL_METERING_MAP_);
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
                ///T_MATERIAL_METERING_MAP_ = null;
            }
        }
        public List<T_MATERIAL_METERING_MAP> get_T_MATERIAL_METERING_MAP()
        {
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
            {
                string strSQL = "select *  from  ORALTL2_ST.T_MATERIAL_METERING_MAP where MAP_ID >=1 and  MAP_ID <=19 order by MAP_ID";
                return iDataBase.GetList<T_MATERIAL_METERING_MAP>(strSQL);
            }
        }

        public void GET_MAT(List<T_MATERIAL_METERING_MAP> T_MATERIAL_METERING_MAP_)
        {
            for (int i = 0; i < T_MATERIAL_METERING_MAP_.Count; i++)
            {
                T_MATERIAL_METERING_MAP M_T_MATERIAL_METERING_MAP = null;
                M_T_MATERIAL_METERING_MAP = new T_MATERIAL_METERING_MAP();
                M_T_MATERIAL_METERING_MAP.MAP_ID = T_MATERIAL_METERING_MAP_[i].MAP_ID;
                M_T_MATERIAL_METERING_MAP.DEVICE_NO = T_MATERIAL_METERING_MAP_[i].DEVICE_NO;
                M_T_MATERIAL_METERING_MAP.DEVICE_NAME = T_MATERIAL_METERING_MAP_[i].DEVICE_NAME;
                M_T_MATERIAL_METERING_MAP.DEVICE_DESCR = T_MATERIAL_METERING_MAP_[i].DEVICE_DESCR;
                M_T_MATERIAL_METERING_MAP.MAP_MAT_CODE = T_MATERIAL_METERING_MAP_[i].MAP_MAT_CODE;
                M_T_MATERIAL_METERING_MAP.MAP_MAT_NAME = T_MATERIAL_METERING_MAP_[i].MAP_MAT_NAME;
                M_T_MATERIAL_METERING_MAP.MAP_BEGIN_TIME = T_MATERIAL_METERING_MAP_[i].MAP_BEGIN_TIME;
                M_T_MATERIAL_METERING_MAP.MAP_END_TIME = T_MATERIAL_METERING_MAP_[i].MAP_END_TIME;
                M_T_MATERIAL_METERING_MAP.CREATE_DATE = T_MATERIAL_METERING_MAP_[i].CREATE_DATE;
                M_T_MATERIAL_METERING_MAP.CREATER = T_MATERIAL_METERING_MAP_[i].CREATER;
                M_T_MATERIAL_METERING_MAP.MAP_STAT = T_MATERIAL_METERING_MAP_[i].MAP_STAT;
                update(M_T_MATERIAL_METERING_MAP);
            }
        }
        public void update(T_MATERIAL_METERING_MAP METERING_MAP)
        {
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                string str_sql = "update  T_MATERIAL_METERING_MAP  set DEVICE_NO='" + METERING_MAP.DEVICE_NO + "',DEVICE_NAME='" + METERING_MAP.DEVICE_NAME + "',DEVICE_DESCR='" + METERING_MAP.DEVICE_DESCR + "',MAP_MAT_CODE='" + METERING_MAP.MAP_MAT_CODE + "',MAP_MAT_NAME='" + METERING_MAP.MAP_MAT_NAME + "' ,MAP_BEGIN_TIME=TO_DATE('" + METERING_MAP.MAP_BEGIN_TIME + "', 'yyyy-mm-dd hh24:mi:ss') ,MAP_END_TIME=TO_DATE('" + METERING_MAP.MAP_END_TIME + "', 'yyyy-mm-dd hh24:mi:ss') ,CREATE_DATE=TO_DATE('" + METERING_MAP.CREATE_DATE + "', 'yyyy-mm-dd hh24:mi:ss'),CREATER='" + METERING_MAP.CREATER + "' where  MAP_ID='" + METERING_MAP.MAP_ID + "' ";
                iDataBase.ExecuteCommand(str_sql);
            }
        }
        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            Writelog(exception.Message + exception.StackTrace);
        }
    }
}
