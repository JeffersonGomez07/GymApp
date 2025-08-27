using DataAccess.Dao;
using DTO;
using System;
using System.Collections.Generic;

namespace DataAccess.Mapper
{
    public class RoutineResultMapper : ICrudStatements, IObjectMapper
    {
        public List<BaseClass> BuildObjects(List<Dictionary<string, object>> objectRows)
        {
            var list = new List<BaseClass>();

            foreach (var row in objectRows)
            {
                var routineResult = BuildObject(row);
                list.Add(routineResult);
            }

            return list;
        }

        public BaseClass BuildObject(Dictionary<string, object> row)
        {
            var routineResult = new RoutineResult
            {
                RoutineId = Convert.ToInt32(row["routine_id"]),
                ExerciseId = Convert.ToInt32(row["exercise_id"]),
                SetsCompleted = row["sets_completed"] != DBNull.Value ? Convert.ToInt32(row["sets_completed"]) : (int?)null,
                RepetitionsCompleted = row["repetitions_completed"] != DBNull.Value ? Convert.ToInt32(row["repetitions_completed"]) : (int?)null,
                WeightUsed = row["weight_used"] != DBNull.Value ? Convert.ToDecimal(row["weight_used"]) : (decimal?)null,
                TimeDuration = row["time_duration"] != DBNull.Value ? TimeSpan.Parse(row["time_duration"].ToString()) : (TimeSpan?)null,
                AmrapTime = row["amrap_time"] != DBNull.Value ? TimeSpan.Parse(row["amrap_time"].ToString()) : (TimeSpan?)null,
                ResultDate = Convert.ToDateTime(row["result_date"])
            };

            return routineResult;
        }

        public SqlOperation GetCreateStatement(BaseClass entityDTO)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "dbo.sp_addRoutineResults";

            var routineResult = (RoutineResult)entityDTO;

            operation.AddIntegerParam("routine_id", routineResult.RoutineId);
            operation.AddIntegerParam("exercise_id", routineResult.ExerciseId);
            operation.AddIntegerParam("sets_completed", routineResult.SetsCompleted);
            operation.AddIntegerParam("repetitions_completed", routineResult.RepetitionsCompleted);
            operation.AddDecimalParam("weight_used", routineResult.WeightUsed);
            operation.AddTimeSpanParam("time_duration", routineResult.TimeDuration);
            operation.AddTimeSpanParam("amrap_time", routineResult.AmrapTime);
            operation.AddDateTimeParam("result_date", routineResult.ResultDate);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseClass entityDTO)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "dbo.sp.retrieveAllRoutineResults";
            return operation;
        }

        public SqlOperation GetRetrieveByIdStatement(int id)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "dbo.sp_RetrieveRoutineResultsByRoutineId";

            operation.AddIntegerParam("result_id", id);
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseClass entityDTO)
        {
            throw new NotImplementedException();
        }
    }
}
