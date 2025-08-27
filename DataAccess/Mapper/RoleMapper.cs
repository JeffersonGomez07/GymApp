using System.Collections.Generic;
using DTO;
using DataAccess.Dao;

namespace DataAccess.Mapper
{
    public class RoleMapper : IObjectMapper
    {
        public SqlOperation GetRoleByUserIdOperation(int userId)
        {
            SqlOperation operation = new SqlOperation
            {
                ProcedureName = "dbo.GetUserRoleName"
            };

            operation.AddIntegerParam("UserId", userId);
            return operation;
        }

        public Role BuildObject(Dictionary<string, object> row)
        {
            return new Role
            {
                Id = Convert.ToInt32(row["role_id"]),
                Name = row["role_name"].ToString()
            };
        }

        public List<Role> BuildObjects(List<Dictionary<string, object>> rows)
        {
            List<Role> roles = new List<Role>();

            foreach (var row in rows)
            {
                var role = BuildObject(row);
                roles.Add(role);
            }

            return roles;
        }

        // Implementación explícita de la interfaz para mantener compatibilidad
        BaseClass IObjectMapper.BuildObject(Dictionary<string, object> row)
        {
            return BuildObject(row);
        }

        List<BaseClass> IObjectMapper.BuildObjects(List<Dictionary<string, object>> rows)
        {
            return new List<BaseClass>(BuildObjects(rows));
        }

        // Método para obtener todos los roles
        public SqlOperation GetRetrieveAllStatement()
        {
            return new SqlOperation
            {
                ProcedureName = "dbo.GetAllRoles"
            };
        }
    }
}