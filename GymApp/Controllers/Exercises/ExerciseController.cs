using Microsoft.AspNetCore.Mvc;
using BL;
using Microsoft.AspNetCore.Cors;
using DTO;

namespace API.Controllers.Exercises
{
    [EnableCors("MyCorsPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExerciseController : Controller
    {
        [HttpPost]
        public IActionResult CreateExercise([FromBody] Exercise exercise)
        {
            try
            {
                ExerciseManager manager = new ExerciseManager();
                manager.CreateExercise(exercise);

                // Devolver una respuesta JSON con un mensaje de éxito
                return Json(new { success = true, message = "Exercise created successfully" });
            }
            catch (Exception ex)
            {
                // Manejar el error y devolver una respuesta JSON con un mensaje de error
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public Exercise GetExercise(int id)
        {
            ExerciseManager manager = new ExerciseManager();
            Exercise exercise = manager.GetExerciseById(id);

            return exercise;
        }
        [HttpGet]
        public IActionResult GetAllExercises()
        {
            try
            {
                ExerciseManager manager = new ExerciseManager();
                List<Exercise> exerciseList = manager.GetAllExercises();

                return Ok(exerciseList); // Asegúrate de devolver un IActionResult
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message }); // Manejo de errores
            }
        }


    }
}
