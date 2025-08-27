using DTO;
using DataAccess.CRUD;

namespace BL
{
    public class RoutineListManager
    {
        public List<RoutineList> GetAllRoutineList()
        {
            RoutineListCrudFactory ex_crud = new RoutineListCrudFactory();
            return ex_crud.RetrieveAll<RoutineList>();
        }
    }
}
