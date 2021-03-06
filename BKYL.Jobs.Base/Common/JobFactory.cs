
using BKYL.Jobs.Base.Model;
using BKYL.Jobs.Base.Utils;
using System;

namespace BKYL.Jobs.Base.Common
{
    public class JobFactory
    {
        public static BaseJob CreateJob(TableConfig item)
        {
            BaseJob baseJob;
            if (item == null)
            {
                throw new Exception("配置对象为空！");
            }
            if (item.RefreshCycle == 0)
            {
                throw new Exception("配置刷新频率不能为零！");
            }
            if (item.RefreshCycle == 0)
            {
                throw new Exception("配置刷新频率不能为零！");
            }
            if (string.IsNullOrEmpty(item.S_DBConnstr) || string.IsNullOrEmpty(item.S_DBConnstr))
            {
                throw new Exception("连接字符串不能为空！");
            }
            if (string.IsNullOrEmpty(item.SelectSql))
            {
                throw new Exception("表字段配置错误！");
            }
            switch (item.JobType)
            {
                case JobType.FullCopy:
                    baseJob = new Plugin.Job_FullCopy();
                    break;
                case JobType.FilterInsert:
                    baseJob = new Plugin.Job_FilterInsert();
                    break;
                case JobType.FilterInsertByDel:
                    baseJob = new Plugin.Job_FilterInsertByDel();
                    break;
                case JobType.FilterInsertUpDel:
                    baseJob = new Plugin.Job_FilterInsertUpdateDel();
                    break;
                case JobType.FilterInsertByFlag:
                    baseJob = new Plugin.Job_FilterInsertbyFlag();
                    break;
                case JobType.FilterInsertBySndFlag:
                    baseJob = new Plugin.Job_FilterInsertbySndFlag();
                    break;
                case JobType.FilterInsertbyTymir21Flag:
                    baseJob = new Plugin.Job_FilterInsertbyTymir21Flag();
                    break;
                case JobType.FilterInsert360:
                    baseJob = new Plugin.Job_FilterInsert360();
                    break;
                ///定制的需要反射出来
                case JobType.CustomJob:
                    baseJob = (BaseJob)ReflectFactory.CreateFullNameObject(item.DllName, item.ClassName);
                    break;
                default:
                    throw new Exception("未知类型的任务");
            }
            baseJob.Task_Fre = item.RefreshCycle;
            baseJob.Task_DelayedTime = item.DelayedTime;
            baseJob.S_DBSource = new DatabaseSource { Connstr = item.S_DBConnstr, DBType = item.s_DBType };
            baseJob.T_DBSource = new DatabaseSource { Connstr = item.T_DBConnstr, DBType = item.t_DBType };
            baseJob.SelectSql = item.SelectSql;
            baseJob.Columns = item.columnNames;
            baseJob.tableConfig = item;
            return baseJob;
        }
    }

}
