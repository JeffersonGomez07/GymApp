using DTO;
using DataAccess.CRUD;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BL
{
    public class MembershipManager
    {
        public string ProcessPayment(PaymentInfo payment)
        {
            MembershipCrudFactory me_crud = new MembershipCrudFactory();

            return me_crud.RegisterMembership(payment);
        }

        public List<Membership> GetAllMemberships()
        {
            MembershipCrudFactory me_crud = new MembershipCrudFactory();
            return me_crud.RetrieveAll<Membership>();
        }

        public string ApproveMembershipPayment(ApprovePaymentRequest payment)
        {
            MembershipCrudFactory me_crud = new MembershipCrudFactory();

            return me_crud.ApproveMembershipPayment(payment);
        }

        public string UploadPaymentReceipt(UploadPaymentRecipt payment)
        {
            MembershipCrudFactory me_crud = new MembershipCrudFactory();

            return me_crud.UploadPaymentReceipt(payment);
        }
    }
}