using Microsoft.AspNetCore.Mvc;
using BL;
using Microsoft.AspNetCore.Cors;
using DTO;

namespace API.Controllers.Members
{
    [EnableCors("MyCorsPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemberController : Controller
    {
        [HttpGet]
        public IActionResult GetAllMembers()
        {
            try
            {
                MemberManager manager = new MemberManager();
                List<Member> memberList = manager.GetAllMembers();

                return Ok(memberList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }
}
