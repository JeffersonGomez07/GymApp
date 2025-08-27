using DataAccess.Crud;
using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;

namespace DataAccess.CRUD
{
    public class RoutineResultCrudFactory : CrudFactory
    {
        private RoutineResultMapper mapper;

        public RoutineResultCrudFactory() : base()
        {
            mapper = new RoutineResultMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseClass entityDTO)
        {
            SqlOperation operation = mapper.GetCreateStatement(entityDTO);
            dao.ExecuteStoredProcedure(operation);
        }

        public override BaseClass RetrieveById(int id)
        {
            SqlOperation operation = mapper.GetRetrieveByIdStatement(id);
            Dictionary<string, object> result = dao.ExecuteStoredProcedureWithUniqueResult(operation);
            var routineResult = mapper.BuildObject(result);
            return routineResult;
        }

        public override List<T> RetrieveAll<T>()
        {
            SqlOperation operation = mapper.GetRetrieveAllStatement();
            List<Dictionary<string, object>> result = dao.ExecuteStoredProcedureWithQuery(operation);
            List<BaseClass> mappedResults = mapper.BuildObjects(result);
            List<T> resultList = new List<T>();

            foreach (var resultItem in mappedResults)
            {
                var convertedResult = (T)Convert.ChangeType(resultItem, typeof(T));
                resultList.Add(convertedResult);
            }

            return resultList;
        }
        public List<T> RetrieveByRoutineId<T>(int routineId)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "RetrieveRoutineResultsByRoutineId";
            operation.AddIntegerParam("routine_id", routineId);

            List<Dictionary<string, object>> results = dao.ExecuteStoredProcedureWithQuery(operation);
            var list = mapper.BuildObjects(results);
            return list.Cast<T>().ToList();
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
