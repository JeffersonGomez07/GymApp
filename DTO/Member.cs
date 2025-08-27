namespace DTO
{
    public class Member : BaseClass
    {
        public int member_id { get; set; }
        public int user_id { get; set; }
        public bool email_verified { get; set; }
        public bool phone_verified { get; set; }
        public bool account_verified { get; set; }
        public string username { get; set; } // Asumiendo que este campo se une en el SP.
    }
}
