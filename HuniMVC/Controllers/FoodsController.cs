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
        private readonly ILogger<FoodsController> _logger;
        private readonly IFoodsRepository _foodsRepository;
        public FoodsController(ILogger<FoodsController> logger, IFoodsRepository foodsRepository)
        {
    
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
            //var model = _context.Foods.Where(x => x.FoodType == "Snack").ToList();
            //SnackViewModel viewModel = new SnackViewModel();
            //viewModel.Foods = new List<Food>(model);
            var model = _foodsRepository.GetSnack();
            return View(model);
        }
        [HttpGet]
        public IActionResult Drink(string Id)
        {
            _logger.LogInformation(@"drinkId={0}", Id);
            var model = _foodsRepository.GetDrink();
            return View(model);
        }

        [HttpGet]
        public IActionResult Popcorn(string Id)
        {

            _logger.LogInformation(@"popcornId={0}", Id);
            var model = _foodsRepository.GetPopcorn();
            return View(model);
        }

   
    }
}
