using HuniMVC.Data;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using HuniMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;
using HuniMVC.ActionResults;
using System.Security.Policy;
using System.Linq;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace HuniMVC.Controllers

{
    public class SignController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly StandardJsonResult _standardJsonResult;
        //private readonly ISessionStore _sessionStore;  ISessionStore를 직접 주입하려고 시도하지 않아야 합니다. 
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _services;

        public SignController(SignInManager<IdentityUser> sugnInManager, UserManager<IdentityUser> userManager, StandardJsonResult standardJsonResult, IHttpContextAccessor httpContextAccessor, IServiceProvider services)
        {
            _signInManager = sugnInManager;
            _userManager = userManager;
            _standardJsonResult = standardJsonResult;
            _httpContextAccessor = httpContextAccessor;
            _services = services;
        }
        [HttpGet]
        public IActionResult SignUp()
        {

            return View();

        }
   
        //[HttpPost]
        //public IActionResult SignUp(string name, string email, string pw, string pwc)
        //{
        //    var user = _context.Users.Where(x => x.Email == email && x.Password == pw);

        //    ViewBag.pwcheck = false;
        //    bool PasswordConfirmed = false;
        //    if (pw == pwc)
        //    {
        //        PasswordConfirmed = true;
        //        if (PasswordConfirmed)
        //        {
        //            ViewBag.pwcheck = true;
        //            return RedirectToAction("Index", "Movies");
        //        }

        //        else
        //        {
        //            ViewBag.pwcheck = false;
        //        }
        //    }

        //    var data = new User
        //        {
        //            UserId = Guid.NewGuid(),
        //            Name = name,
        //            Email = email,
        //            Password = pw,
        //            PasswordConfirmed = "true"
        //        };
        //        _context.Users.Add(data);
        //        _context.SaveChanges();
        //        return View(data);   
        //}
 
        //signup기능 구현할때, 메모리에 유저 데이터가 저장되게끔해서 로그인된 유저인지 아닌지 확인시키도록한다.
        [HttpPost]
        public async Task<IActionResult> SignUp(string name, string email, string pw)
        {
            var user = new IdentityUser { UserName = name, Email = email };
            var result = await _userManager.CreateAsync(user, pw);
            
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false); // isPersistent: false는 세션 쿠키를 사용하여 로그인 정보를 저장 , 브라우저가 닫히면 세션이 종료
                return RedirectToAction("Index", "Movies");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View();
            }
        }
        [HttpGet]
        public IActionResult SignOut()
        {
            return View();

        }

        [HttpPost]
        public IActionResult SignOut(Guid Id)
        {
            //var userId = _context.Users.FirstOrDefault(x => x.UserId == Id);
            //_context.Users.Remove(userId);
            //_context.SaveChanges();
           
            return View();
        }

        [HttpGet]
        public IActionResult LogIn()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn([FromForm] string email, string pw, IdentityUser user)
        {

            var users = await _userManager.GetUserIdAsync(user);
            var IdentityEmail = await _userManager.FindByEmailAsync(email);
            if (IdentityEmail == null)
            {
                ViewBag.failcount = true;
                return View();
            }

            if (IsUserLockedOut(IdentityEmail))
            {
                ViewBag.failtime = true;
                return View();
            }
     

            await ResetLockoutInfoIfRequired(IdentityEmail); // 로그인 제한 삭제
            //Salt 암호화
            //var newpw = new PasswordHasher<IdentityUser>();
            //string saltpw = newpw.HashPassword(IdentityEmail, pw);
            //var result = await AttemptSignIn(email, pw);
            //var result = await _userManager.CheckPasswordAsync(IdentityEmail, pw);// 단순 비밀번호검증, 불리언 결과 반환
            var result = await _signInManager.PasswordSignInAsync(IdentityEmail, pw, isPersistent: false, lockoutOnFailure: false);// 로그인 세션 관리, 계정 잠금처리, 복합결과 반환
          
            if (result.Succeeded) //로그인 성공
            {
                ISession session = _services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
                //_httpContextAccessor.HttpContext.Session.SetString("UserId", users);
                session.SetString("UserId", users);
                return RedirectToAction("Index", "Movies");
            }
            else //로그인 실패
            {
                await HandleFailedSignIn(IdentityEmail); //3회 이상 실패 + 3분간 제한
                
                return View();
            }
        }
        [HttpGet]
        public IActionResult LogOut()
        {

            return View();
        }
        [HttpPost]
        public IActionResult LogOut([FromForm] string email, string pw, IdentityUser user)
        {
            _httpContextAccessor.HttpContext.Session.Clear();
            return RedirectToAction("LogIn", "Sign");
        }

        private async Task HandleFailedSignIn(IdentityUser IdentityEmail)
        {
            await _userManager.AccessFailedAsync(IdentityEmail);
            var failedCount = await _userManager.GetAccessFailedCountAsync(IdentityEmail);
            //await ResetLockoutInfoIfRequired(IdentityEmail);
            if (failedCount >= 3)
            {
                LockoutUser(IdentityEmail); // 3분간 제한
                ViewBag.discorduser = false;
                ViewBag.failcount = true;
                //ViewBag.failcountmesaage = failedCount;
            }
            else
            {
                ViewBag.failcountmesaage = failedCount;
                ViewBag.discorduser = true;
                
            }
        }

        private void LockoutUser(IdentityUser IdentityEmail)
        {
            var localTime = DateTime.Now.AddMinutes(3);
            var lockoutEnd = new DateTimeOffset(localTime, TimeZoneInfo.Local.GetUtcOffset(localTime));
            _userManager.SetLockoutEndDateAsync(IdentityEmail, lockoutEnd);
        }

        private bool IsUserLockedOut(IdentityUser IdentityEmail)
        {
            var lockoutEnd = _userManager.GetLockoutEndDateAsync(IdentityEmail).Result?.ToLocalTime();
            return lockoutEnd >= DateTime.Now;
        }

        private async Task ResetLockoutInfoIfRequired(IdentityUser IdentityEmail)
        {
            var lockoutEnd = await _userManager.GetLockoutEndDateAsync(IdentityEmail);
            if (lockoutEnd?.ToLocalTime() < DateTime.Now)
            {
                await _userManager.ResetAccessFailedCountAsync(IdentityEmail);
                await _userManager.SetLockoutEndDateAsync(IdentityEmail, null);
            }
        }


        //Hash 암호화
        //private async Task<Microsoft.AspNetCore.Identity.SignInResult> AttemptSignIn(string email, string pw)
        //{
        //    var hashpw = EncryptPw(pw);
        //    return await _signInManager.PasswordSignInAsync(email, hashpw, false, true);
        //}
        //public string EncryptPw(string pw)
        //{
        //    SHA256Managed sha = new SHA256Managed();
        //    byte[] encryptBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(pw)); //sha256를 사용하기 위해 바이트로 변환해줘야한다.
        //    String encryptStringPW = Convert.ToBase64String(encryptBytes);
        //    return encryptStringPW;
        //}

    }
}
