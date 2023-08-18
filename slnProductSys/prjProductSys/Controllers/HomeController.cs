using Microsoft.AspNetCore.Mvc;
using prjProductSys.Models;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace prjProductSys.Controllers
{
    public class HomeController : Controller
    {
        DbProductContext _context;

        void SetCategory()
        {
            ViewBag.Category = _context.TCategories.ToList();
        }

        public HomeController(DbProductContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id=1)
        {
            SetCategory();
            ViewBag.CategoryName = _context.TCategories.FirstOrDefault(m=>m.CategoryId == id).CategoryName;  
            var product=_context.TProducts.Where(m=>m.CategoryId == id).ToList();   
            return View(product);
        }


        public IActionResult Login()
        {
            SetCategory();
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Uid, string Pwd)
        {
            SetCategory();
            // 依uid和pwd取得會員
            var member = _context.TMembers
                .FirstOrDefault(m => m.Uid == Uid && m.Pwd == Pwd );
            // 若member非null，表示有該位會員
            if (member != null)
            {
                IList<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, member.Uid),
                        new Claim(ClaimTypes.Role, member.Role)
                    };
                var claimsIndentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { IsPersistent = true };
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIndentity),
                    authProperties);
               
                return RedirectToAction("Index", member.Role);   //前往會員對應的控制器
            }
            ViewBag.Msg = "帳密錯誤，請重新檢查";
            return View();
        }


        //Home/Logout，登出作業
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        //權限不足會執行此處
        public IActionResult NoAuthorization()
        {
            SetCategory();
            return View();
        }



        //Home/MemberCreate，顯示新增會員畫面
        public IActionResult MemberCreate()
        {
            SetCategory();
            return View();
        }

        //會員新增畫面按下Sumbit執行
        [HttpPost]
        public IActionResult MemberCreate(TMember member)
        {
            SetCategory();
            if (ModelState.IsValid)
            {
                try
                {
                    member.Role = "Member";
                    _context.TMembers.Add(member);
                    _context.SaveChanges();
                    return RedirectToAction("Login");
                }
                catch (Exception ex)
                {
                    ViewBag.Msg = "會員新增失敗，帳號可能重複";
                }
            }
            return View(member);
        }







    }
}
