using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Dao
{
    public class SqlOperation
    {
        public string ProcedureName { get; set; }
        public List<SqlParameter> parameters;

        public SqlOperation()
        {
            parameters = new List<SqlParameter>();
        }

        public void AddVarcharParam(string parameterName, string paramValue)
        {
            parameters.Add(new SqlParameter("@" + parameterName, paramValue));
        }

        public void AddTextParam(string parameterName, string paramValue)
        {
            parameters.Add(new SqlParameter("@" + parameterName, paramValue));
        }

        public void AddDateTimeParam(string parameterName, DateTime paramValue)
        {
            parameters.Add(new SqlParameter("@" + parameterName, paramValue));
        }

        public void AddTimeSpanParam(string parameterName, TimeSpan? paramValue)
        {
            if (paramValue.HasValue)
            {
                parameters.Add(new SqlParameter("@" + parameterName, SqlDbType.Time) { Value = paramValue.Value });
            }
            else
            {
                parameters.Add(new SqlParameter("@" + parameterName, SqlDbType.Time) { Value = DBNull.Value });
            }
        }


        public void AddIntegerParam(string parameterName, int? paramValue)
        {
            if (paramValue.HasValue)
            {
                parameters.Add(new SqlParameter("@" + parameterName, SqlDbType.Int) { Value = paramValue.Value });
            }
            else
            {
                parameters.Add(new SqlParameter("@" + parameterName, SqlDbType.Int) { Value = DBNull.Value });
            }
        }

        public void AddDecimalParam(string parameterName, decimal? paramValue)
        {
            if (paramValue.HasValue)
            {
                parameters.Add(new SqlParameter("@" + parameterName, SqlDbType.Decimal)
                {
                    Value = paramValue.Value,
                    Precision = 5,
                    Scale = 2
                });
            }
            else
            {
                parameters.Add(new SqlParameter("@" + parameterName, SqlDbType.Decimal)
                {
                    Value = DBNull.Value,
                    Precision = 5,
                    Scale = 2
                });
            }
        }





    }
}