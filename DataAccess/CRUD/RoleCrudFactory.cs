using System.Collections.Generic;
using DataAccess.Crud;
using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;

namespace DataAccess.CRUD
{
    public class RoleCrudFactory : CrudFactory
    {
        private readonly RoleMapper _mapper;

        public RoleCrudFactory()
        {
            _mapper = new RoleMapper();
            dao = SqlDao.GetInstance();
        }

        public Role GetRoleByUserId(int userId)
        {
            var operation = _mapper.GetRoleByUserIdOperation(userId);
            var result = dao.ExecuteStoredProcedureWithUniqueResult(operation);
            return _mapper.BuildObject(result);
        }

        public override List<T> RetrieveAll<T>()
        {
            var operation = _mapper.GetRetrieveAllStatement();
            var results = dao.ExecuteStoredProcedureWithQuery(operation);
            var roles = _mapper.BuildObjects(results);

            var castedRoles = new List<T>();
            foreach (var role in roles)
            {
                if (role is T castedRole)
                {
                    castedRoles.Add(castedRole);
                }
            }

            return castedRoles;
        }

        public override void Create(BaseClass entityDTO)
        {
            // Implementación vacía o real si se necesita
            throw new NotImplementedException();
        }

        public override void Update(BaseClass entityDTO)
        {
            // Implementación vacía o real si se necesita
            throw new NotImplementedException();
        }

        public override void Delete(BaseClass entityDTO)
        {
            // Implementación vacía o real si se necesita
            throw new NotImplementedException();
        }

        public override BaseClass RetrieveById(int id)
        {
            // Implementación vacía o real si se necesita
            throw new NotImplementedException();
        }
    }
}