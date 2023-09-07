using HuniMVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace HuniMVC.Models
{
    public static class SnackSeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new HuniMVCContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<HuniMVCContext>>()))
            {
                // Look for any movies.
                if (context.Foods.Any())
                {
                    return;   // DB has been seeded
                }

                context.Foods.AddRange(
                    new Food
                    {
                        FoodName = "과자",
                        FoodType = "Snack",
                        FoodDescription = "맛있는 과자",
                        Price = 2.29,
                        FoodId = Guid.NewGuid(),
                    },

                    new Food
                    {
                        FoodName = "어니언 팝콘",
                        FoodType = "Popcorn",
                        FoodDescription = "어니언 맛이나는 팝콘",
                        Price = 10.31,
                        FoodId = Guid.NewGuid(),
                    },

                    new Food
                    {
                        FoodName = "오징어 땅콩",
                        FoodType = "Snack",
                        FoodDescription = "오징어와 땅콩 ",
                        Price = 17.99,
                        FoodId = Guid.NewGuid(),
                    },

                    new Food
                    {
                        FoodName = "When Harry Met Sally",
                        FoodType = "Drink",
                        FoodDescription = "콜라",
                        Price = 3.00,
                        FoodId = Guid.NewGuid(),
                    }
                );
                context.SaveChanges();
            }
        }
    }
}