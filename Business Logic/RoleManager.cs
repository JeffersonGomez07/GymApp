using DTO;
using DataAccess.CRUD;
using System.Collections.Generic;

namespace BL
{
    public class RoleManager
    {
        private readonly RoleCrudFactory _roleCrudFactory;

        public RoleManager()
        {
            _roleCrudFactory = new RoleCrudFactory();
        }

        public List<Role> GetAllRoles()
        {
            return _roleCrudFactory.RetrieveAll<Role>();
        }
    }
}