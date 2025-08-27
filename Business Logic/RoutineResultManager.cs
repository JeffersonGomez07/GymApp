using DTO;
using DataAccess.CRUD;

namespace BL
{
    public class RoutineResultManager
    {
        public void AddRoutineResults(RoutineResult routineResult)
        {
            RoutineResultCrudFactory ex_crud = new RoutineResultCrudFactory();
            ex_crud.Create(routineResult);
        }

        public List<RoutineResult> GetResultsByRoutineId(int routineId)
        {
            RoutineResultCrudFactory crud = new RoutineResultCrudFactory();
            return crud.RetrieveByRoutineId<RoutineResult>(routineId);
        }
 
        public void CreateRoutineResult(RoutineResult routineResult)
        {
             RoutineResultCrudFactory resultCrud = new RoutineResultCrudFactory();
             resultCrud.Create(routineResult);
        }
        

    }
}



