using HuniMVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HuniMVC.Models;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;

namespace HuniMVC.Controllers
{
    public class CommentController : Controller
    {
        private readonly HuniMVCContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CommentController(HuniMVCContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public ActionResult _CommentList(Guid messageId)
        {
            //var currentUserId2 = _userManager?.FindByIdAsync(movieId);

            var currentUserId = User.Identity?.Name ?? string.Empty; // 로그인된 유저
            var models = _context.Messages.Where(x => x.MessageId == messageId && currentUserId.Any()).ToList();
            ViewBag.MessageId = messageId;
          
            return PartialView("_CommentList", models);
        }

        [HttpPost]
        public JsonResult CommentAdd(Guid? messageId, string comment, int movieId)
        {


            var movie = _context.Movies.FirstOrDefault(x => x.MovieId == movieId);
            var models = new Message
            {
                MovieId = movie.MovieId,
                //Message = message,
                Body = comment,
                MessageId = messageId,
                date = DateTime.Now
            };
            _context.Add(models);
            _context.SaveChanges();
            return Json(models);
        }
        [HttpPost]
        public JsonResult CommentDelete(Guid? messageId, int commentId) // parameters["commentId"] 
        {
            var comments = _context.Messages.FirstOrDefault(x => x.MessageId == messageId && x.Id == commentId);
            _context.Remove(comments);
            _context.SaveChanges();
            return Json(comments);
        }
       
    }
}
