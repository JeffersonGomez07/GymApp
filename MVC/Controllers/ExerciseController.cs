using Microsoft.AspNetCore.Mvc;

namespace GymApp.Controllers
{
    public class ExerciseController : Controller
    {

        public async Task<IActionResult> Create()
        {
            return View();
        }
        

    }
}
