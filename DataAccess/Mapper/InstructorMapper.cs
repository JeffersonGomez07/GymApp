using DataAccess.Dao;
using DTO;
using System.Collections.Generic;

namespace DataAccess.Mapper
{
    public class InstructorMapper : ICrudStatements, IObjectMapper
    {
        public List<BaseClass> BuildObjects(List<Dictionary<string, object>> objectRows)
        {
            var list = new List<BaseClass>();
            foreach (var row in objectRows)
            {
                var instructor = BuildObject(row);
                list.Add(instructor);
            }
            return list;
        }

        public BaseClass BuildObject(Dictionary<string, object> row)
        {
            var instructor = new Instructor
            {
                InstructorId = int.Parse(row["instructor_id"].ToString()),
                UserId = int.Parse(row["user_id"].ToString()),
                EmailVerified = bool.Parse(row["email_verified"].ToString()),
                PhoneVerified = bool.Parse(row["phone_verified"].ToString()),
                Username = row["username"].ToString()
            };
            return instructor;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            SqlOperation operation = new SqlOperation
            {
                ProcedureName = "dbo.sp_getAllInstructors"
            };
            return operation;
        }

         
        public SqlOperation GetCreateStatement(BaseClass entityDTO) { throw new NotImplementedException(); }
        public SqlOperation GetDeleteStatement(BaseClass entityDTO) { throw new NotImplementedException(); }
        public SqlOperation GetRetrieveByIdStatement(int id) { throw new NotImplementedException(); }
        public SqlOperation GetUpdateStatement(BaseClass entityDTO) { throw new NotImplementedException(); }
    }
}
