namespace DTO
{
    public class PaymentInfo : BaseClass
    {
        public int UserId { get; set; }
        public string MembershipType { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string ReceiptImage { get; set; }
    }

}



