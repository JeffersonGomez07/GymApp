namespace DTO
{
    public class Membership : BaseClass
    {
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Membership_type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Payment_date { get; set; }
        public string Status { get; set; }
        public int User_id { get; set; }
        public int Payment_id { get; set; }
        public int Membership_id { get; set; }
        public string Receipt_image { get; set; }
    }

    public class ApprovePaymentRequest : BaseClass
    {
        public int User_id { get; set; }
        public int Payment_id { get; set; }
        public int Membership_id { get; set; }
    }

    public class UploadPaymentRecipt : BaseClass
    {
        public int Payment_id { get; set; }
        public string Payment_receipt { get; set; }
    }
}