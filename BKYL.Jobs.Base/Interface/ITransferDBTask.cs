using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.Jobs.Base.Interface
{
    public interface ITransferDBTask
    {
        void InitTask();
        void RunTask(DateTime currentTime);
        void RunTaskException(DateTime currentTime, Exception exception);
    }
}
