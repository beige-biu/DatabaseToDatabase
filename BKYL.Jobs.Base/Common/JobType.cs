using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.Jobs.Base.Common
{
    public enum JobType
    {
        /// <summary>
        /// 整表复制
        /// </summary>
        FullCopy = 1,
        /// <summary>
        /// 过滤插入
        /// </summary>
        FilterInsert = 3,
        /// <summary>
        /// 删除之后过滤插入
        /// </summary>
        FilterInsertByDel = 2,
        /// <summary>
        /// 对比后插入更新删除
        /// </summary>
        FilterInsertUpDel = 4,
        ///<summary>
        /// 将标志位不为1的数据插入
        ///</summary>
        FilterInsertByFlag = 6,
        ///<summary>
        ///判断COMM_SND_FLAG的值来进行数据同步
        ///</summary>
        FilterInsertBySndFlag = 7,
        ///<summary>
        ///同步xct1tymir21数据
        FilterInsertbyTymir21Flag = 21,
        /// </summary>
        /// <summary>
        /// 自定义插入
        /// </summary>
        CustomJob = 99
    }
}
