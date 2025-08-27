using DataAccess.Crud;
using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;
using Microsoft.VisualBasic;

namespace DataAccess.CRUD
{
    public class ExerciseCrudFactory : CrudFactory
    {
        private ExerciseMapper mapper;

        public ExerciseCrudFactory() : base()
        {
            mapper = new ExerciseMapper();
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
        public override void Delete(BaseClass entityDTO)
        {
            throw new NotImplementedException();
        }

        public override BaseClass RetrieveById(int id)
        {
            SqlOperation operation = mapper.GetRetrieveByIdStatement(id);
          
            Dictionary<string, object> result = dao.ExecuteStoredProcedureWithUniqueResult(operation);
            var Exercise = mapper.BuildObject(result);

            return Exercise;

        }

        public override List<T> RetrieveAll<T>()
        {
            SqlOperation operation = mapper.GetRetrieveAllStatement();

            List<Dictionary<string, object>> result = dao.ExecuteStoredProcedureWithQuery(operation);

            List<BaseClass> mappedExercises = mapper.BuildObjects(result);

            List<T> exerciseList = new List<T>();

            foreach (var exercise in mappedExercises)
            {
                var convertedRoutine = (T)Convert.ChangeType(exercise, typeof(T));
                exerciseList.Add(convertedRoutine);
            }

            return exerciseList;
        }


    }

}