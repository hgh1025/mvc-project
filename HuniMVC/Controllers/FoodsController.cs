using HuniMVC.Data;
using HuniMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Linq;
using System.Collections.Generic;
using HuniMVC.ViewModel;
using HuniMVC.Repository;

namespace HuniMVC.Controllers
{
    public class FoodsController : Controller
    {
        private readonly HuniMVCContext _context;
        private readonly ILogger<FoodsController> _logger;
        private readonly IFoodsRepository _foodsRepository;
        public FoodsController(HuniMVCContext context, ILogger<FoodsController> logger, IFoodsRepository foodsRepository)
        {
            _context = context;
            _logger = logger;
            _foodsRepository = foodsRepository;
        }
        public ActionResult Index()
        {
            return View();

        }

        [HttpGet]
        public IActionResult Snack(string Id)
        {
            _logger.LogInformation(@"snackId={0}", Id);
            var model = _context.Foods.Where(x => x.FoodType == "Snack").ToList();
            SnackViewModel viewModel = new SnackViewModel();
            viewModel.Foods = new List<Food>(model);
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Drink(string name)
        {
            _foodsRepository.GetSnack(name);
            ViewBag.Name = name;
            return View();
        }

        [HttpGet]
        public IActionResult Popcorn(string name)
        {
            ViewBag.Name = name;
            return View();
        }

   
    }
}
