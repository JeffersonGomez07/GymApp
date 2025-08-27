using DTO;
using DataAccess.CRUD;

namespace BL
{
    public class MemberManager
    {
        public List<Member> GetAllMembers()
        {
            MemberCrudFactory memberCrud = new MemberCrudFactory();
            return memberCrud.RetrieveAll<Member>();
        }
    }
}
