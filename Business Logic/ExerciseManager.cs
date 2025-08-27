
using DTO;
using DataAccess.CRUD;

namespace BL
{
    public class ExerciseManager
    {
        public void CreateExercise(Exercise Exercise)
        {
            ExerciseCrudFactory ex_crud = new ExerciseCrudFactory();
            ex_crud.Create(Exercise);
        }

        public Exercise GetExerciseById(int id)
        {
            ExerciseCrudFactory ex_crud = new ExerciseCrudFactory();
            return (Exercise)ex_crud.RetrieveById(id);
        }
        public List<Exercise> GetAllExercises()
        {
            ExerciseCrudFactory ex_crud = new ExerciseCrudFactory();
            return ex_crud.RetrieveAll<Exercise>();
        }


    }
}