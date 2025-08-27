using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.Controllers;

public class MemberController : Controller
{
   
    public IActionResult MemberPage()
    {
        return View();
    }
}

