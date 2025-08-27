using DataAccess.Dao;
using DTO;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;


namespace DataAccess.Mapper
{
    public class MembershipMapper : ICrudStatements, IObjectMapper

    {

        public List<BaseClass> BuildObjects(List<Dictionary<string, object>> objectRows)
        {
            var list = new List<BaseClass>();
            
            foreach( var row in objectRows)
            {
                var membership = BuildObject(row);
                list.Add(membership);
            }

            return list;
        }

        public BaseClass BuildObject(Dictionary<string, object> result)
        {
            var membership = new Membership()
            {
                First_name = result["first_name"].ToString(),
                Last_name = result["last_name"].ToString(),
                Membership_type = result["membership_type"].ToString(),
                Amount = Decimal.Parse(result["amount"].ToString()),
                Receipt_image = result["receipt_image"].ToString(),
                Payment_date = DateTime.Parse(result["payment_date"].ToString(), null, System.Globalization.DateTimeStyles.RoundtripKind),
                Status = result["payment_status"].ToString(),
                User_id = int.Parse(result["user_id"].ToString()),
                Payment_id = int.Parse(result["payment_id"].ToString()),
                Membership_id = int.Parse(result["membership_id"].ToString())
            };
            return membership;
        }

        public SqlOperation GetCreateStatement(BaseClass entityDTO, SqlParameter errorMessage)
        {
            SqlOperation operation = new SqlOperation
            {
                ProcedureName = "dbo.sp_ProcessMembershipPayment"
            };

            PaymentInfo payment = (PaymentInfo)entityDTO;

            operation.AddIntegerParam("user_id ", payment.UserId);
            operation.AddVarcharParam("membership_type", payment.MembershipType);
            operation.AddDecimalParam("amount", payment.Amount);
            operation.AddVarcharParam("payment_method", payment.PaymentMethod);
            operation.AddVarcharParam("receipt_image", payment.ReceiptImage);

            operation.parameters.Add(errorMessage);

            return operation;
        }

        public SqlOperation GetApproveMembershipPayment(BaseClass entityDTO, SqlParameter errorMessage)
        {
            SqlOperation operation = new SqlOperation
            {
                ProcedureName = "dbo.sp_approveMembershipPayment"
            };

            ApprovePaymentRequest payment = (ApprovePaymentRequest)entityDTO;

            operation.AddIntegerParam("user_id ", payment.User_id);
            operation.AddIntegerParam("payment_id", payment.Payment_id);
            operation.AddIntegerParam("membership_id", payment.Membership_id);

            operation.parameters.Add(errorMessage);

            return operation;
        }

        public SqlOperation GetUploadPaymentReceipt(BaseClass entityDTO, SqlParameter errorMessage)
        {
            SqlOperation operation = new SqlOperation
            {
                ProcedureName = "dbo.sp_uploadPaymentReceipt"
            };

            UploadPaymentRecipt payment = (UploadPaymentRecipt)entityDTO;

            operation.AddIntegerParam("payment_id", payment.Payment_id);
            operation.AddVarcharParam("payment_receipt ", payment.Payment_receipt);

            operation.parameters.Add(errorMessage);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseClass entityDTO)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "dbo.sp_getAllMembershipPayments";

            return operation;
        }

        public SqlOperation GetRetrieveByIdStatement(int Id)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "dbo.sp_getRoutine";

            operation.AddIntegerParam("Id", Id);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseClass entityDTO)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetCreateStatement(BaseClass entityDTO)
        {
            throw new NotImplementedException();
        }
    }
}
