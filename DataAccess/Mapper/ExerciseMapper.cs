using DataAccess.Dao;
using DTO;
using System.Diagnostics.Metrics;


namespace DataAccess.Mapper
{
    public class ExerciseMapper : ICrudStatements, IObjectMapper

    {

        public List<BaseClass> BuildObjects(List<Dictionary<string, object>> objectRows)
        {
            var list = new List<BaseClass>();
            
            foreach( var row in objectRows)
            {
                var exercise = BuildObject(row);
                list.Add(exercise);
            }

            return list;
        }

        public BaseClass BuildObject(Dictionary<string, object> result)
        {
            var Exercise = new Exercise()
            {
                exerciseId = int.Parse(result["exercise_id"].ToString()),
                exerciseTypeId = int.Parse(result["exercise_type_id"].ToString()),
                name = result["name"].ToString(),
                description = result["description"].ToString(),
                primaryMuscle = result["primary_muscle"].ToString(),
            };

            return Exercise;
        }



        public SqlOperation GetCreateStatement(BaseClass entityDTO)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "dbo.sp_addExercise";

            Exercise Exercise = (Exercise)entityDTO;

            operation.AddIntegerParam("exercise_type_id", Exercise.exerciseTypeId);
            operation.AddVarcharParam("name", Exercise.name);
            operation.AddVarcharParam("description", Exercise.description);
            operation.AddVarcharParam("primary_muscle", Exercise.primaryMuscle);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseClass entityDTO)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveByIdStatement(int Id)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "dbo.sp_getExercise";

            operation.AddIntegerParam("Id", Id);

            return operation;
        }
        public SqlOperation GetRetrieveAllStatement()
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "dbo.sp_getAllExercises";

            return operation;
        }
        public SqlOperation GetUpdateStatement(BaseClass entityDTO)
        {
            throw new NotImplementedException();
        }
    }
}
