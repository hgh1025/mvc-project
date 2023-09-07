using HuniMVC.Models;
using HuniMVC.ViewModel;
using Microsoft.AspNetCore.Mvc;

// 관리자 페이지
namespace HuniMVC.Repository
{
    public interface IFoodsRepository
    {
        Food? Foods { get; }
        void FoodId(Guid foodId);

        IEnumerable<Food> GetSnack() ;
        IEnumerable<Food> GetDrink();
        IEnumerable<Food> GetPopcorn() ;

    }
}
