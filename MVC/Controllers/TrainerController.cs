using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.Controllers;

public class TrainerController : Controller
{
   
    public IActionResult TrainerPage()
    {
        return View();
    }
}

