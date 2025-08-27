
using DTO;
using DataAccess.CRUD;

namespace BL
{
    public class RoutineExerciseManager
    {

        public void AddExerciseToRoutine(RoutineExercise routineExercise)
        {
            RoutineExerciseCrudFactory ex_crud = new RoutineExerciseCrudFactory();
            ex_crud.AddExerciseToRoutine(routineExercise);  // Asegúrate de que este método esté implementado
        }

        public List<RoutineExercise> GetExercisesForRoutine(int routineId)
        {
            RoutineExerciseCrudFactory factory = new RoutineExerciseCrudFactory();
            return factory.RetrieveByRoutineId(routineId);
        }


    }
}