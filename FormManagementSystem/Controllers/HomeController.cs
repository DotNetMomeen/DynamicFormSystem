using Microsoft.AspNetCore.Mvc;

namespace FormManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        //[Route("")]
        //[Route("Home")]
        //[Route("Home/Index")]
        //public IActionResult Home()
        //{
        //    return View();
        //}
        //public IActionResult Index()
        //{
        //    return View();
        //    return View("~/Views/Home/Index.cshtml");
        //    return RedirectToAction("Login", "Account");
        //}
        //public IActionResult Index()
        //{
        //    return View();

        //}

        public IActionResult Index()
        {
            return View("./Views/Home/Index.cshtml");
        }
    }
}
