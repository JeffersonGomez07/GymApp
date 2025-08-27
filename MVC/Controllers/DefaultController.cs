using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.Controllers;

public class DefaultController : Controller
{
   
    public IActionResult DefaultPage()
    {
        return View();
    }
}

