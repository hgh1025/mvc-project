using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuniMVC.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public string OrderType { get; set; }

        	public enum Orders
        {
            /// <summary>
            /// 1주간
            /// </summary>
            Snack = 0,

        /// <summary>
        /// 3개월
        /// </summary>
		Drink = 1,
		Popcorn = 2

    }


    }
}
