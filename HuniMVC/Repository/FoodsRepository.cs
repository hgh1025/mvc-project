using HuniMVC.Data;
using HuniMVC.Models;
using HuniMVC.ViewModel;
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
        public Food? Foods => throw new NotImplementedException();

        public void FoodId(Guid foodId)
        {

            throw new NotImplementedException();
        }

        public IEnumerable<Food> GetSnack()
        {
            return _context.Foods.Where(x => x.FoodType == "Snack").ToList();
        }

        public IEnumerable<Food> GetDrink()
        {
             return _context.Foods.Where(x => x.FoodType == "Drink").ToList();
        }

        public IEnumerable<Food> GetPopcorn()
        {
            return _context.Foods.Where(x => x.FoodType == "Popcorn").ToList();
        }
    }
}
