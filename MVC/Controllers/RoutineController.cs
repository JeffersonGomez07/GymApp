using Microsoft.AspNetCore.Mvc;

namespace GymApp.Controllers
{
    public class RoutineController : Controller
    {

        public async Task<IActionResult> Create()
        {
            return View();
        }
        public async Task<IActionResult> routineList()
        {
            return View();
        }
        public IActionResult AddExerciseRoutine(int routineId)
        {
            // Lógica para manejar la adición de ejercicios a la rutina
            return View();
        }

        public async Task<IActionResult> RecordResults()
        {
            return View();
        }
        public async Task<IActionResult> Results()
        {
            return View();
        }
    }


}
