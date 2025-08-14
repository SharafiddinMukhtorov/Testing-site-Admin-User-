using Microsoft.AspNetCore.Mvc;

namespace TestFullStack.Controllers;

public class LoginController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            ViewBag.Error = "Please enter a username.";
            return View();
        }

        username = username.ToLower();

        if (username == "admin")
            return RedirectToAction("Index", "Admin");
        else if (username == "user")
            return RedirectToAction("Index", "User");
        else
        {
            ViewBag.Error = "Invalid username!";
            return View();
        }
    }
}
