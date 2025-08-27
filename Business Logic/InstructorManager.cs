using DTO;
using DataAccess.CRUD;

namespace BL
{
    public class InstructorManager
    {
        public List<Instructor> GetAllInstructors()
        {
            InstructorCrudFactory crud = new InstructorCrudFactory();
            return crud.RetrieveAll<Instructor>();
        }
    }
}
