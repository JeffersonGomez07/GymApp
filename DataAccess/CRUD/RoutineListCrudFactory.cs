using DataAccess.Crud;
using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;

namespace DataAccess.CRUD
{
    public class RoutineListCrudFactory : CrudFactory
    {
        private RoutineListMapper mapper;

        public RoutineListCrudFactory() : base()
        {
            mapper = new RoutineListMapper();
            dao = SqlDao.GetInstance();
        }

        public override List<T> RetrieveAll<T>()
        {
            SqlOperation operation = mapper.GetRetrieveAllStatement();

            List<Dictionary<string, object>> result = dao.ExecuteStoredProcedureWithQuery(operation);

            List<BaseClass> mappedRoutines = mapper.BuildObjects(result);

            List<T> routineList = new List<T>();

            foreach (var routine in mappedRoutines)
            {
                var convertedRoutine = (T)Convert.ChangeType(routine, typeof(T));
                routineList.Add(convertedRoutine);
            }

            return routineList;
        }

        public override BaseClass RetrieveById(int id)
        {
            SqlOperation operation = mapper.GetRetrieveByIdStatement(id);

            Dictionary<string, object> result = dao.ExecuteStoredProcedureWithUniqueResult(operation);
            var routine = mapper.BuildObject(result);

            return routine;
        }

        public override void Create(BaseClass entityDTO)
        {
            throw new NotImplementedException("Not applicable for RoutineList");
        }

        public override void Update(BaseClass entityDTO)
        {
            throw new NotImplementedException();
        }

        public override void Delete(BaseClass entityDTO)
        {
            throw new NotImplementedException();
        }
    }
}
