using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace HuniMVC.Controllers
{
    public class FoodController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Index()
        {
            return View();

        }

        public IActionResult Snack(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello" + name;
            ViewData["NumTimes"] = numTimes;
            ViewBag.Name = name;
            return View();
        }
        public IActionResult Drink(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello" + name;
            ViewData["NumTimes"] = numTimes;
            ViewBag.Name = name;
            return View();
        }
    }
}
