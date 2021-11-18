using System;
using BFES.DataAccess;

namespace BKYL.Jobs.Base.Utils
{
    public static class DalFactory
    {
        public static IDataBase GreateIDataBase(DatabaseSource DBSource)
        {
            return DataBaseFactory.GreateDataBaseFactory(DBSource.Connstr, DBSource.DBType);
        }
    }
}
