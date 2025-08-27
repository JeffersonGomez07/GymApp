
using DTO;
using DataAccess.CRUD;

namespace BL
{
    public class RoutineManager
    {
        //Este metodo se encarga de pedir la informacion para el controlador
        public void CreateRoutine(Routine Routine)
        {
            RoutineCrudFactory ex_crud = new RoutineCrudFactory();
            ex_crud.Create(Routine);
        }

        public Routine GetRoutineById(int id)
        {
            RoutineCrudFactory ex_crud = new RoutineCrudFactory();
            return (Routine)ex_crud.RetrieveById(id);
        }

        public List<Routine> GetAllRoutines()
        {
            RoutineCrudFactory ex_crud = new RoutineCrudFactory();
            return ex_crud.RetrieveAll<Routine>();
        }

        public void DeleteRoutine(int routineId)
        {
            RoutineCrudFactory crudFactory = new RoutineCrudFactory();
            Routine routine = new Routine { routineId = routineId };
            crudFactory.Delete(routine);
        }


    }
}