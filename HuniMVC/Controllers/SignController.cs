using HuniMVC.Data;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using HuniMVC.Models;

namespace HuniMVC.Controllers

{
    public class SignController : Controller
    {
        private readonly HuniMVCContext _context;

        public SignController(HuniMVCContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult SignUp() { 
            return View();

        }
        [HttpPost]
        public IActionResult SignUp(string name, string email, string pw)
        {
          
            var data = new User
                {
                    UserId = Guid.NewGuid(),
                    Name = name,
                    Email = email,
                    Password = pw,
                    PasswordConfirmed = "true"
                };
                _context.Users.Add(data);
                _context.SaveChanges();
                return View(data);   
        }

        [HttpGet]
        public IActionResult SignOut()
        {
            return View();

        }

        [HttpPost]
        public IActionResult SignOut(Guid Id)
        {
            var userId = _context.Users.FirstOrDefault(x => x.UserId == Id);
            _context.Users.Remove(userId);
            _context.SaveChanges();

            return View();
        }
    }
}
