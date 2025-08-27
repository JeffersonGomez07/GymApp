namespace DTO
{
    public class User : BaseClass
    {
        public int Role_id { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone_number { get; set; }
        public DateTime Birthdate { get; set; }
        public string Profile_image { get; set; }
        public string Id_image { get; set; }
    }
}