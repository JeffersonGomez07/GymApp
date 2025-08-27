using Microsoft.AspNetCore.Mvc;
using DTO;
using BL;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;

        public AdminApiController()
        {
            _userManager = new UserManager();
            _roleManager = new RoleManager();
        }

        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            var users = _userManager.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("GetRoles")]
        public IActionResult GetRoles()
        {
            var roles = _roleManager.GetAllRoles();
            return Ok(roles);
        }

        [HttpPost("AssignRole")]
        public IActionResult AssignRole([FromBody] AssignRoleRequest request)
        {
            _userManager.AssignRole(request.UserId, request.RoleId);
            return Ok();
        }
    }
}