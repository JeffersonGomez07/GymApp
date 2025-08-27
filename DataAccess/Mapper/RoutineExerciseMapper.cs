using DataAccess.Dao;
using DTO;
using System.Collections.Generic;

namespace DataAccess.Mapper
{
    public class RoutineExerciseMapper : ICrudStatements, IObjectMapper
    {

        public List<BaseClass> BuildObjects(List<Dictionary<string, object>> objectRows)
        {
            var list = new List<BaseClass>();

            foreach (var row in objectRows)
            {
                var routineExercise = BuildObject(row);
                list.Add(routineExercise);
            }

            return list;
        }

        public BaseClass BuildObject(Dictionary<string, object> row)
        {
            var routineExercise = new RoutineExercise
            {
                routineId = Convert.ToInt32(row["routine_id"]),
                exerciseId = Convert.ToInt32(row["exercise_id"]),
                exerciseName = row["exercise_name"].ToString(),
                exerciseTypeId = Convert.ToInt32(row["exercise_type_id"]),
                sets = row["sets"] != DBNull.Value ? Convert.ToInt32(row["sets"]) : (int?)null,
                repetitions = row["repetitions"] != DBNull.Value ? Convert.ToInt32(row["repetitions"]) : (int?)null,
                weight = row["weight"] != DBNull.Value ? Convert.ToDecimal(row["weight"]) : (decimal?)null,
                timeDuration = row["time_duration"] != DBNull.Value ? TimeSpan.Parse(row["time_duration"].ToString()) : (TimeSpan?)null,
                amrapTime = row["amrap_time"] != DBNull.Value ? TimeSpan.Parse(row["amrap_time"].ToString()) : (TimeSpan?)null
            };

            return routineExercise;
        }



        public SqlOperation GetCreateStatement(BaseClass entityDTO)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "dbo.sp_addExerciseToRoutine";

            RoutineExercise routineExercise = (RoutineExercise)entityDTO;

            operation.AddIntegerParam("routine_id", routineExercise.routineId);
            operation.AddIntegerParam("exercise_id", routineExercise.exerciseId);
            operation.AddIntegerParam("exercise_type_id", routineExercise.exerciseTypeId);
            operation.AddIntegerParam("sets", routineExercise.sets);
            operation.AddIntegerParam("repetitions", routineExercise.repetitions);
            operation.AddDecimalParam("weight", routineExercise.weight);
            operation.AddTimeSpanParam("time_duration", routineExercise.timeDuration);
            operation.AddTimeSpanParam("amrap_time", routineExercise.amrapTime);

            return operation;
        }


        public SqlOperation GetDeleteStatement(BaseClass entityDTO)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "dbo.sp_getAllRoutineExercises";

            return operation;
        }

        public SqlOperation GetRetrieveByIdStatement(int id)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "dbo.sp_getRoutineExerciseById";

            operation.AddIntegerParam("id", id);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseClass entityDTO)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveByRoutineIdStatement(int routineId)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "dbo.sp_getExercisesByRoutineId";
            operation.AddIntegerParam("routine_id", routineId);
            return operation;
        }

    }

}
