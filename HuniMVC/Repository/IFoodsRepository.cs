using HuniMVC.Models;
using Microsoft.AspNetCore.Mvc;

// 관리자 페이지
namespace HuniMVC.Repository
{
    public interface IFoodsRepository
    {
      IEnumerable<Food>? Food { get; }
        void FoodId(Guid foodId);

        void GetSnack(string name) ;
        void GetDrink(Food food) ;
        void GetPopcorn(Food food) ;

    }
}
