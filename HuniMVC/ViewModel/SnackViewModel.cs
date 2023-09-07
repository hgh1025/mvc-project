using HuniMVC.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HuniMVC.ViewModel
{
    public class SnackViewModel
    {
        public string FoodType { get; set; }
        public string FoodName { get; set; }
        public string FoodDescription { get; set; }
        public double Price { get; set; }
        public SnackViewModel()
        {
            Foods = new List<Food>();
        }
        public List<Food> Foods { get; set; }
   

        
    }

}
