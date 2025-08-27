using DataAccess.Crud;
using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;

namespace DataAccess.CRUD
{
    public class MemberCrudFactory : CrudFactory
    {
        private MemberMapper mapper;

        public MemberCrudFactory() : base()
        {
            mapper = new MemberMapper();
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

            List<BaseClass> mappedMembers = mapper.BuildObjects(result);

            List<T> memberList = new List<T>();

            foreach (var member in mappedMembers)
            {
                var convertedMember = (T)Convert.ChangeType(member, typeof(T));
                memberList.Add(convertedMember);
            }

            return memberList;
        }
    }
}
