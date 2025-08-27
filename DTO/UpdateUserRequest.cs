using System;

namespace DTO
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone_number { get; set; }
        public DateTime Birthdate { get; set; }
        // No incluimos password, profile_image, ni id_image
    }
}
