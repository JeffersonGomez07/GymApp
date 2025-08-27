using DataAccess.Dao;
using DTO;

namespace DataAccess.Mapper
{
    public class RoutineListMapper : ICrudStatements, IObjectMapper
    {
        public List<BaseClass> BuildObjects(List<Dictionary<string, object>> objectRows)
        {
            var list = new List<BaseClass>();

            foreach (var row in objectRows)
            {
                var routine = BuildObject(row);
                list.Add(routine);
            }

            return list;
        }

       
        
            public BaseClass BuildObject(Dictionary<string, object> result)
            {
                var routine = new RoutineList()
                {
                    routineId = int.Parse(result["routine_id"].ToString()),
                    memberUsername = result["member_username"].ToString(),
                    instructorUsername = result["instructor_username"].ToString(), // Agregar este campo
                    name = result["name"].ToString(),
                    description = result["description"].ToString(),
                    creationDate = DateTime.Parse(result["creation_date"].ToString(), null, System.Globalization.DateTimeStyles.RoundtripKind)
                };
                return routine;
            }
            
        

        public SqlOperation GetRetrieveAllStatement()
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "dbo.sp_getAllRoutineList";

            return operation;
        }

        public SqlOperation GetRetrieveByIdStatement(int Id)
        {
            throw new NotImplementedException();

        }

        public SqlOperation GetCreateStatement(BaseClass entityDTO)
        {
            throw new NotImplementedException("Not applicable for RoutineList");
        }

        public SqlOperation GetUpdateStatement(BaseClass entityDTO)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetDeleteStatement(BaseClass entityDTO)
        {
            throw new NotImplementedException();
        }
    }
}
