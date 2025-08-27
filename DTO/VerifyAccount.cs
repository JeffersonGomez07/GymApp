namespace DTO
{
    public class VerifyAccountRequest
    {
        public string Email { get; set; }
    }

    public class VerifyAccountConfirm
    {
        public string Otp { get; set; }
    }
}



