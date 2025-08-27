using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.Controllers;

public class MembershipController : Controller
{
   
    public IActionResult ConfirmMembership()
    {
        return View();
    }
}

