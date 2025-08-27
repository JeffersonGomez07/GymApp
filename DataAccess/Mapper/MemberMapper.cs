using DataAccess.Dao;
using DTO;
using System.Collections.Generic;

namespace DataAccess.Mapper
{
    public class MemberMapper : ICrudStatements, IObjectMapper
    {
        public List<BaseClass> BuildObjects(List<Dictionary<string, object>> objectRows)
        {
            var list = new List<BaseClass>();

            foreach (var row in objectRows)
            {
                var member = BuildObject(row);
                list.Add(member);
            }

            return list;
        }

        public BaseClass BuildObject(Dictionary<string, object> row)
        {
            var member = new Member()
            {
                member_id = int.Parse(row["member_id"].ToString()),
                user_id = int.Parse(row["user_id"].ToString()),
                email_verified = bool.Parse(row["email_verified"].ToString()),
                phone_verified = bool.Parse(row["phone_verified"].ToString()),
                account_verified = bool.Parse(row["account_verified"].ToString()),
                username = row["username"].ToString() // Asumiendo que este campo se une en el SP.
            };

            return member;
        }

        public SqlOperation GetCreateStatement(BaseClass entityDTO)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveByIdStatement(int id)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "dbo.sp_getAllMembers";

            return operation;
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
