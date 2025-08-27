namespace DTO
{
    public class Instructor : BaseClass
    {
        public int InstructorId { get; set; }
        public int UserId { get; set; }
        public bool EmailVerified { get; set; }
        public bool PhoneVerified { get; set; }
        public string Username { get; set; }
    }
}
