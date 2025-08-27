using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.Controllers;

public class ReceptionistController : Controller
{
   
    public IActionResult ReceptionistPage()
    {
        return View();
    }
}

