using DataAccess.Crud;
using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;
using Microsoft.VisualBasic;

namespace DataAccess.CRUD
{
    public class RoutineCrudFactory : CrudFactory
    {
        private RoutineMapper mapper;

        public RoutineCrudFactory() : base()
        {
            mapper = new RoutineMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseClass entityDTO)
        {
            SqlOperation operation = mapper.GetCreateStatement(entityDTO);
            dao.ExecuteStoredProcedure(operation);
        }
        public override void Update(BaseClass entityDTO)
        {
            throw new NotImplementedException();
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
            var Routine = mapper.BuildObject(result);

            return Routine;

        }
        public void AddExerciseToRoutine(RoutineExercise routineExercise)
        {
            RoutineExerciseMapper exerciseMapper = new RoutineExerciseMapper();
            SqlOperation operation = exerciseMapper.GetCreateStatement(routineExercise);
            dao.ExecuteStoredProcedure(operation);
        }

        public override void Delete(BaseClass entityDTO)
        {
            Routine routine = (Routine)entityDTO;
            SqlOperation operation = mapper.GetDeleteStatement(routine);
            dao.ExecuteStoredProcedure(operation);
        }



    }
}