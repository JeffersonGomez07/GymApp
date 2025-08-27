using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.Controllers;

public class RutineController : Controller
{
   
    public IActionResult CreateRutine()
    {
        return View();
    }

}

