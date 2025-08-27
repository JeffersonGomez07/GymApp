using DataAccess.Crud;
using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;
using System.Collections.Generic;

namespace DataAccess.CRUD
{
    public class InstructorCrudFactory : CrudFactory
    {
        private InstructorMapper mapper;

        public InstructorCrudFactory() : base()
        {
            mapper = new InstructorMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseClass entityDTO)
        {
            throw new NotImplementedException();
        }

        public override void Delete(BaseClass entityDTO)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            SqlOperation operation = mapper.GetRetrieveAllStatement();
            List<Dictionary<string, object>> result = dao.ExecuteStoredProcedureWithQuery(operation);
            List<BaseClass> mappedInstructors = mapper.BuildObjects(result);

            List<T> instructorList = new List<T>();
            foreach (var instructor in mappedInstructors)
            {
                instructorList.Add((T)Convert.ChangeType(instructor, typeof(T)));
            }

            return instructorList;
        }

        public override BaseClass RetrieveById(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update(BaseClass entityDTO)
        {
            throw new NotImplementedException();
        }
    }
}
