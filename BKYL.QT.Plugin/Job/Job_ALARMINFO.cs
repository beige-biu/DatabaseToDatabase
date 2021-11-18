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
    /// <summary>
    /// 李海森  设备运行率
    /// </summary>
    public class Job_ALARMINFO : BaseJob
    {
        // 原表
        MATERIAL_STORAGE_BIN MATERIAL_STORAGE_BIN_ = null;
        public override void InitTask()
        {
        }
        public override void RunTask(DateTime currentTime)
        {
            string log = "";
            try
            {
                //源数据库 表
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
                {


                }
                //目标数据库 表
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    List<ALARM_INFORMATION> ypllist = null;
                    List<ALARM_INFORMATION> HGlist = null;
                    List<ALARM_INFORMATION> GYlist = null;
                    List<ALARM_INFORMATION> PHlist = null;
                    List<ALARM_INFORMATION> ZQlist = null;
                    List<ALARM_INFORMATION> BSlist = null;
                    List<ALARM_INFORMATION> CPlist = null;
                    double YPLAL;
                    double HGAL;
                    double GYAL;
                    double PHAL;
                    double ZQAL;
                    double BSAL;
                    double CPAL;


                    //  currentTime =("2020-06-30 08:40:00");
                    //currentTime = Convert.ToDateTime("2020-06-30 09:33:00");
                    double ypl = CONFIG_ALARMINFO(2, iDataBase);
                    double HG = CONFIG_ALARMINFO(3, iDataBase);
                    double GY = CONFIG_ALARMINFO(4, iDataBase);
                    double PH = CONFIG_ALARMINFO(5, iDataBase);
                    double ZQ = CONFIG_ALARMINFO(6, iDataBase);
                    double BS = CONFIG_ALARMINFO(7, iDataBase);
                    double CP = CONFIG_ALARMINFO(8, iDataBase);


                    ypllist = GET_ALARM_INFORMATION(2, iDataBase, currentTime);
                    HGlist = GET_ALARM_INFORMATION(3, iDataBase, currentTime);
                    GYlist = GET_ALARM_INFORMATION(4, iDataBase, currentTime);
                    PHlist = GET_ALARM_INFORMATION(5, iDataBase, currentTime);
                    ZQlist = GET_ALARM_INFORMATION(6, iDataBase, currentTime);
                    BSlist = GET_ALARM_INFORMATION(7, iDataBase, currentTime);
                    CPlist = GET_ALARM_INFORMATION(8, iDataBase, currentTime);

                    //y预配料
                    if (ypllist.Count > 0)
                    {
                        YPLAL = (ypl - ypllist.Count) / ypl * 100;
                    }
                    else
                    {
                        YPLAL = 100;
                    }
                    //烘干
                    if (HGlist.Count > 0)
                    {
                        HGAL = ((HG - HGlist.Count) / HG) * 100;
                    }
                    else
                    {
                        HGAL = 100;
                    }
                    //辊压
                    if (GYlist.Count > 0)
                    {
                        GYAL = (GY - GYlist.Count) / GY * 100;
                    }
                    else
                    {
                        GYAL = 100;

                    }
                    //配混
                    if (PHlist.Count > 0)
                    {
                        PHAL = (PH - PHlist.Count) / PH * 100;
                    }
                    else
                    {
                        PHAL = 100;

                    }
                    //造球
                    if (ZQlist.Count>0)
                    {
                        ZQAL = (ZQ - ZQlist.Count) / ZQ * 100;
                    }
                    else
                    {
                        ZQAL = 100;

                    }

                    //焙烧
                    if (BSlist.Count > 0)
                    {
                        BSAL = (BS - BSlist.Count) / BS * 100;
                    }
                    else
                    {
                        BSAL = 100;

                    }
                    //成品
                    if (CPlist.Count > 0)
                    {
                        CPAL = (CP - CPlist.Count) / CP * 100;
                    }
                    else
                    {
                        CPAL = 100;

                    }



                    set_save(YPLAL, HGAL, GYAL, PHAL, ZQAL, BSAL, CPAL, currentTime);
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


       public void set_save(double YPLAL, double HGAL, double GYAL, double PHAL, double ZQAL, double BSAL, double CPAL,DateTime currentTime)
        {
            string log = "";
            TC_RUN_RATE TC_RUN_RATE_ = null;
            try
            {
                TC_RUN_RATE_ = new TC_RUN_RATE();
                TC_RUN_RATE_.TIMESTAMP = currentTime;
                TC_RUN_RATE_.TC_ROLLING = GYAL;
                TC_RUN_RATE_.TC_PRE_MATERIAL = YPLAL;
                TC_RUN_RATE_.TC_MATERIAL = PHAL;
                TC_RUN_RATE_.TC_DRY = HGAL;
                TC_RUN_RATE_.TC_BLEND = 100;
                TC_RUN_RATE_.TC_PELLET = ZQAL;
                TC_RUN_RATE_.TC_ROAS = BSAL;
                TC_RUN_RATE_.TC_PRODUCT = CPAL;
            }
            catch (Exception ee)
            {

                throw;
            }
            if (Set_Admin_add(TC_RUN_RATE_))
            {
                log = "保存完成";
            }
            else
            {
                log = "保存失败";
            }

        }


        public bool Set_Admin_add(TC_RUN_RATE model)
        {
            bool listmodel = false;

            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                listmodel = (bool)iDataBase.Insert(model);
                return listmodel;
            }
        }
        public List<ALARM_INFORMATION> GET_ALARM_INFORMATION(int M_alarm_type, IDataBase iDataBase,DateTime currentTime)
        {
            try
            {
                DateTime starttime;
                DateTime endtime;
                starttime = currentTime.AddMinutes(-1);
                endtime = currentTime;
                string selectRoasterSql = string.Format("select alarm_id from ALARM_INFORMATION where PROCESS_ID ={0} and  timestamp>{1} and timestamp <={2} group by alarm_id ", M_alarm_type, DataBaseFactory.ConvertTimeString(starttime, DataBaseType.Oracle), DataBaseFactory.ConvertTimeString(endtime, DataBaseType.Oracle));

                return iDataBase.GetList<ALARM_INFORMATION>(selectRoasterSql);
                

                //List<ALARM_INFORMATION> roasterModel = iDataBase.GetList<ALARM_INFORMATION>(selectRoasterSql);
                //return roasterModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int CONFIG_ALARMINFO(int M_alarm_type, IDataBase iDataBase)
        {
            try
            {
                string selectRoasterSql = string.Format("select count(*) from CONFIG_ALARMINFO where PROCESS_ID ={0}", M_alarm_type);
                int roasterModel = iDataBase.GetInt(selectRoasterSql);
                return roasterModel;
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
