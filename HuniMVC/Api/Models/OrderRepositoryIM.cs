using HuniMVC.Api.Models;

namespace HuniMVC.Api.Models
{
    public class OrderRepositoryIM : IOrderRepository
    {
        private Dictionary<int, Order> _orderlist;

        public OrderRepositoryIM()
        {
            _orderlist = new Dictionary<int, Order>();
        }

        public Order this[int id] => _orderlist.ContainsKey(id) ? _orderlist[id] : null;

        public IEnumerable<Order> Orders => _orderlist.Values;

        public Order Add(Order newOrder)
        {
            if (newOrder.Id == 0)  // 새로 생성된 Order라면
            {
                int key = _orderlist.Count; // 최고 큰 Id 값을 시작으로

                while (_orderlist.ContainsKey(key)) key++; // 아직 사용하지 않은 큰 값을
                newOrder.Id = key;  // 추가되는 Order의 Id로 확정
            }

            _orderlist[newOrder.Id] = newOrder; // 오더의 Id를 키로 사전에 저장.

            return newOrder;
        }

        public void Delete(int id) => _orderlist.Remove(id);

        public Order Update(Order order) => Add(order);
    }


        }
    

