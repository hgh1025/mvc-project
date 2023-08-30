using Microsoft.AspNetCore.Mvc;
using HuniMVC.ActionResults;
namespace HuniMVC.Infrasturcture
{
    public class BaseController : Controller
    {
        protected StandardJsonResult JsonValidationError()
        {
            return JsonValidationError();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
