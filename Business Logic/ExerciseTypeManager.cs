
using DTO;
using DataAccess.CRUD;

namespace BL
{
    public class ExerciseTypeManager
    {
        public List<ExerciseType> GetAllExerciseTypes()
        {
            ExerciseTypeCrudFactory ex_crud = new ExerciseTypeCrudFactory();
            return ex_crud.RetrieveAll<ExerciseType>();
        }
    }
}