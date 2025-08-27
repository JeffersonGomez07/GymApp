using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using DTO;
using BL;

namespace API.Controllers.Users
{
    [EnableCors("MyCorsPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly UserManager _userManager;

        // Constructor
        public UsersController()
        {
            _userManager = new UserManager();
        }

        [HttpPost]
        public ActionResult RegisterUser(User user)
        {
            string error = _userManager.RegisterUser(user);

            if (user == null)
            {
                return BadRequest("User data is null.");
            }

            return Ok(new { errorMessage = error });
        }

        [HttpPost]
        public IActionResult UpdateUser([FromBody] UpdateUserRequest user)
        {
            var existingUser = _userManager.GetUserById(user.Id);
            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            // Solo actualizar los campos que se envían en la solicitud
            existingUser.First_name = user.First_name;
            existingUser.Last_name = user.Last_name;
            existingUser.Username = user.Username;
            existingUser.Email = user.Email;
            existingUser.Phone_number = user.Phone_number;
            existingUser.Birthdate = user.Birthdate;

            _userManager.UpdateUser(existingUser);

            return Ok(new { message = "User updated successfully" });
        }


        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            User user = _userManager.Login(request.Username, request.Password);

            if (request == null)
            {
                return BadRequest("Invalid login request.");
            }

            if (user == null)
            {
                return Unauthorized();
            }

            var RoleName = _userManager.GetUserRoleName(user.Id);

            return Ok(new { userId = user.Id, username = user.Username, role = RoleName });
        }

        [HttpPost]
        public IActionResult ResetPassword([FromBody] ResetPassword request)
        {
            UserManager manager = new UserManager();
            string error = manager.UpdatePassword(request);

            if (request == null || string.IsNullOrEmpty(request.Token) || string.IsNullOrEmpty(request.NewPassword))
            {
                return BadRequest("Invalid request to reset password.");
            }

            return Ok(new { errorMessage = error });
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.GetAllUsers();

            if (users == null || users.Count == 0)
            {
                return NotFound("No users found.");
            }

            return Ok(users);
        }

        [HttpGet]
        public IActionResult VerifyAccount([FromQuery] string otp)
        {
            UserManager manager = new UserManager();
            string error = manager.VerifyAccount(otp);

            if (string.IsNullOrEmpty(otp))
            {
                return BadRequest("Invalid request to verify account.");
            }

            return Ok(new { errorMessage = error });
        }

        [HttpGet]
        public User GetUser(int id)
        {
            return _userManager.GetUserById(id);
        }
    }
}
