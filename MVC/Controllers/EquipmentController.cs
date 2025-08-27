using Microsoft.AspNetCore.Mvc;

namespace GymUI.Controllers
{
    public class EquipmentController : Controller
    {
        public IActionResult Equipments()
        {
            return View(); 
        }
    }
}
