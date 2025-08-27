namespace DTO
{
    public class ResetPasswordRequest
    {
        public string Email { get; set; }
        public string ResetUrl { get; set; }
    }

    public class ResetPassword
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
        public string confirmPassword { get; set; }
    }
}



