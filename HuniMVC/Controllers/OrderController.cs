using HuniMVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HuniMVC.Models;
using System.Xml.Linq;


namespace HuniMVC.Controllers
{
    public class ChartController : Controller
    {
        private readonly HuniMVCContext _context;
        public ChartController(HuniMVCContext context)
        {
            _context = context;
        }
        public ActionResult ChartList(Guid messageId)
        {
            var currentUserId = User.Identity?.Name ?? string.Empty; // 로그인된 유저
            var models = _context.Messages.Where(x => x.MessageId == messageId).ToList();
            ViewBag.MessageId = messageId;
          
            return PartialView("CharttList", models);
        }

        [HttpPost]
        public JsonResult ChartAdd(Guid? orderId)
        {


            var orders = _context.Orders.FirstOrDefault(x => x.OrderId == orderId);
            var models = new Order
            {
                OrderId = orders.OrderId,
                OrderType = orders.OrderType,

            };
            _context.Add(models);
            _context.SaveChanges();
            return Json(models);
        }
        [HttpPost]
        public JsonResult ChartDelete(Guid? orderId) // parameters["commentId"] 
        {
            var comments = _context.Orders.FirstOrDefault(x => x.OrderId == orderId);
            _context.Remove(comments);
            _context.SaveChanges();
            return Json(comments);
        }
       
    }
}
