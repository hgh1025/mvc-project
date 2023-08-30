using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace HuniMVC.Api.Models
{
    public interface IOrderRepository
    {
        IEnumerable<Order> Orders { get; } // 모든 주문을 반환한다
        Order this[int id] { get; }       // 하나의 주문을 반환한다.
        Order Add(Order newOrder);  // 주문을 추가한다. 추가가 성공하면 추가된 주문을 반환한다.
        Order Update(Order order);  // 주문을 수정한다. 수정이 성공하면, 수정된 주문을 반환한다.
        void Delete(int id);        // 주문을 삭제한다.
    }



    public class Order
    {
        public int Id { get; set; }  // int 형식의 기본값인 0으로 자동 초기화
        public string ClientName { get; set; }
        public string Menu { get; set; }
        public int NoOfMenu { get; set; }

        public Order(string clientName, string menuName, int noOfMenu)
        {
            ClientName = clientName;
            Menu = menuName;
            NoOfMenu = noOfMenu;
        }


    }
}
