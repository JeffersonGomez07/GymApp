using Microsoft.AspNetCore.Mvc;

namespace GymApp.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult AdminPage()
        {
            return View();
        }

        public IActionResult AssignRole()
        {
            return View();
        }

        public IActionResult ManageUser()
        {
            return View();
        }


    }

}