using System.Data;
using System.Data.SqlClient;
using DataAccess.Crud;
using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;
using Microsoft.VisualBasic;

namespace DataAccess.CRUD
{
    public class MembershipCrudFactory : CrudFactory
    {
        private MembershipMapper mapper;

        public MembershipCrudFactory() : base()
        {
            mapper = new MembershipMapper();
            dao = SqlDao.GetInstance();
        }

        public string RegisterMembership(BaseClass entityDTO)
        {
            var errorMessage = new SqlParameter("@errorMessage", SqlDbType.VarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            SqlOperation operation = mapper.GetCreateStatement(entityDTO, errorMessage);
            dao.ExecuteStoredProcedure(operation);

            string error = errorMessage.Value as string;
            return error;
        }

        public string ApproveMembershipPayment(BaseClass entityDTO)
        {
            var errorMessage = new SqlParameter("@errorMessage", SqlDbType.VarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            SqlOperation operation = mapper.GetApproveMembershipPayment(entityDTO, errorMessage);
            dao.ExecuteStoredProcedure(operation);

            string error = errorMessage.Value as string;
            return error;
        }

        public string UploadPaymentReceipt(BaseClass entityDTO)
        {
            var errorMessage = new SqlParameter("@errorMessage", SqlDbType.VarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            SqlOperation operation = mapper.GetUploadPaymentReceipt(entityDTO, errorMessage);
            dao.ExecuteStoredProcedure(operation);

            string error = errorMessage.Value as string;
            return error;
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

        public override List<T> RetrieveAll<T>()
        {
            SqlOperation operation = mapper.GetRetrieveAllStatement();

            List<Dictionary<string, object>> result = dao.ExecuteStoredProcedureWithQuery(operation);

            List<BaseClass> mappedMembership = mapper.BuildObjects(result);

            List<T> membershipList = new List<T>();

            foreach (var membership in mappedMembership)
            {
                var convertedRoutine = (T)Convert.ChangeType(membership, typeof(T));
                membershipList.Add(convertedRoutine);
            }

            return membershipList;
        }

        public override BaseClass RetrieveById(int id)
        {
            SqlOperation operation = mapper.GetRetrieveByIdStatement(id);
          
            Dictionary<string, object> result = dao.ExecuteStoredProcedureWithUniqueResult(operation);
            var Routine = mapper.BuildObject(result);

            return Routine;

        } 
    }
}