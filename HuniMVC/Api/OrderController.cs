using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using HuniMVC.Api.Models;
using Newtonsoft.Json;
//ASP.NET Core에서는 기본적으로 JSON 형식을 사용하여 요청과 응답을 처리합니다. 이는 내부적으로 JSON 형식을 직렬화하고 역직렬화하는 미들웨어가 자동으로 처리하기 때문입니다.
//[FromBody] 어트리뷰트가 있는 경우, 요청 본문의 JSON 데이터가 해당 파라미터 타입으로 자동 역직렬화됩니다. 반대로 응답 데이터는 자동으로 JSON 형식으로 직렬화되어 클라이언트로 전송됩니다.
namespace HuniMVC.Api
{
    [Route("api/[controller]")]
    [ApiController]
    //    API 컨트롤러는 Http 통신을 담당하는 ControllerBase 클래스에 기반하지만, MVC 컨트롤러는 Controller 클래스에 기반한다.
    //사실, Controller 클래스는 ControllerBase를 기반하고, 거기에 페이지에 관한 로직이 더해진 것이다.
    //이 말은 사실, MVC 컨트롤러도 API 컨트롤러로 동작하는데 아무런 문제가 없다는 뜻이다.그러한 이유로, MVC 컨트롤러가 HTML 문서 응답과 JSON 응답을 모두 해야 한다면, Controller 클래스를 파생하여, MVC 컨트롤러로 정의하면 된다.
    public class OrderController : ControllerBase 
    {
        private IOrderRepository _orderRepo;

        public OrderController(IOrderRepository repo) // 저장소 객체 의존성 주입, 생성자 
        {
            _orderRepo = repo;
        }

        [HttpGet]
        public IEnumerable<Order> Get() => _orderRepo.Orders;

        [HttpGet("{id}")]
        public Order Get(int id) => _orderRepo[id];

        //[HttpPost]
        //public Order Post([FromForm] string jsonData)
        //{
        //    Order order = JsonConvert.DeserializeObject<Order>(jsonData);
        //    return _orderRepo.Add(new Order(order.ClientName, order.Menu, order.NoOfMenu));
        //}


        [HttpPost]
        public Order Post([FromForm] Order order) => _orderRepo.   //FromForm이 아니라 FromBody로 설정하면 postman에서 form-data 형식으로 데이터를 post하면  request가 json으로 가지 않기 때문에 header정보가 일치하지 않게 된다.
            Add(new Order(order.ClientName, order.Menu, order.NoOfMenu));

        //[HttpPost]
        //public Order Post([FromForm] Order order) => _orderRepo.Add(order);   //FromForm이 아니라 FromBody로 설정하면 postman에서 form-data 형식으로 데이터를 post하면  request가 json으로 가지 않기 때문에 header정보가 일치하지 않게 된다.
  
        [HttpPut]
        public Order Put([FromBody] Order order) => _orderRepo.Update(order);

        [HttpPatch("{id}")]
        public StatusCodeResult Patch(int id, [FromBody] JsonPatchDocument<Order> patch)
        {
            Order order = Get(id);

            if (order != null)
            {
                patch.ApplyTo(order);
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public void Delete(int id) => _orderRepo.Delete(id);
    }


    }

