using DataAccess.Crud;
using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;
using Microsoft.VisualBasic;

namespace DataAccess.CRUD
{
    public class ExerciseTypeCrudFactory : CrudFactory
    {
        private ExerciseTypeMapper mapper;

        public ExerciseTypeCrudFactory() : base()
        {
            mapper = new ExerciseTypeMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseClass entityDTO)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();

        }

        public override List<T> RetrieveAll<T>()
        {
            SqlOperation operation = mapper.GetRetrieveAllStatement();

            List<Dictionary<string, object>> result = dao.ExecuteStoredProcedureWithQuery(operation);

            List<BaseClass> mappedExercisesTypes = mapper.BuildObjects(result);

            List<T> exerciseTypeList = new List<T>();

            foreach (var exerciseType in mappedExercisesTypes)
            {
                var convertedExerciseType = (T)Convert.ChangeType(exerciseType, typeof(T));
                exerciseTypeList.Add(convertedExerciseType);
            }

            return exerciseTypeList;
        }


    }

}