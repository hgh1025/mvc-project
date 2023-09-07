using HuniMVC.Data;
using HuniMVC.Models;
using Microsoft.AspNetCore.Mvc;

// 관리자 페이지
namespace HuniMVC.Repository
{
    public class FoodsRepository : IFoodsRepository
    {
        private readonly HuniMVCContext _context;
        public FoodsRepository(HuniMVCContext context)
        {
            _context = context;
        }
        public IEnumerable<Food>? Food => throw new NotImplementedException();

     

        public void FoodId(Guid foodId)
        {

            throw new NotImplementedException();
        }

        public void GetSnack(string name)
        {
            var model = _context.Foods.Where(x => x.FoodType == "Snack").ToList();
        }

        public void GetDrink(Food food)
        {
            var model = _context.Foods.Where(x => x.FoodType == "Drink").ToList();
        }

        public void GetPopcorn(Food food)
        {
            var model = _context.Foods.Where(x => x.FoodType == "Popcorn").ToList();
        }
    }
}
