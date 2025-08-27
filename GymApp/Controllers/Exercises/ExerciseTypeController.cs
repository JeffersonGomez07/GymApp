using Microsoft.AspNetCore.Mvc;
using BL;
using Microsoft.AspNetCore.Cors;
using DTO;

namespace API.Controllers.Exercises
{
    [EnableCors("MyCorsPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExerciseTypeController : Controller
    {
        [HttpGet]
        public IActionResult GetAllExerciseTypes()
        {
            try
            {
                ExerciseTypeManager manager = new ExerciseTypeManager();
                List<ExerciseType> exerciseTypeList = manager.GetAllExerciseTypes();

                return Ok(exerciseTypeList); // Asegúrate de devolver un IActionResult
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message }); // Manejo de errores
            }
        }


    }
}
