using DTO;
using DataAccess.CRUD;

namespace BL
{
    public class CreateRoutineManager
    {
        //Este metodo se encarga de pedir la informacion para el controlador
        public void CreateRoutine(Routine Routine)
        {
            RoutineCrudFactory ex_crud = new RoutineCrudFactory();
            ex_crud.Create(Routine);
        }


    }
}