using Microsoft.AspNetCore.Mvc;
using BL;
using Microsoft.AspNetCore.Cors;
using DTO;

namespace API.Controllers
{
    [EnableCors("MyCorsPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InstructorController : Controller
    {
        [HttpGet]
        public IActionResult GetAllInstructors()
        {
            try
            {
                InstructorManager manager = new InstructorManager();
                List<Instructor> instructorList = manager.GetAllInstructors();

                return Ok(instructorList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }
}
