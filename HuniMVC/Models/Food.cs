using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuniMVC.Models
{
    public class Food
    {
        public Guid FoodId { get; set; }
        public string FoodType { get; set; }
        public string FoodName { get; set;}
        public string FoodDescription { get; set;}
        public double Price { get; set;}

        public List<Food> Foods { get; set; }

     

    }
}
