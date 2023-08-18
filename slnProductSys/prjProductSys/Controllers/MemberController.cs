using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using prjProductSys.Models;

using prjProductSys.ViewModels;



namespace prjProductSys.Controllers
{
    [Authorize(Roles = "Member")]
    public class MemberController : Controller
    {

        DbProductContext _context;
        public MemberController(DbProductContext context)
        {
            _context = context;
        }

        void SetCategory()
        {
            ViewBag.Category = _context.TCategories.ToList();
        }


        //--------------------------------------

        //public IActionResult Index(int id = 0)
        //{
        //    SetCategory();
        //    return View();
        //}

        public IActionResult Index(int id = 1)
        {
            SetCategory();
            ViewBag.CategoryName = _context.TCategories.FirstOrDefault(m => m.CategoryId == id).CategoryName;
            var product = _context.TProducts.Where(m => m.CategoryId == id).ToList();
            return View(product);
        }


        public IActionResult MemberEdit()
        {
            SetCategory();
            string uid = User.Identity.Name;
            var member = _context.TMembers.FirstOrDefault(m => m.Uid == uid);
            return View(member);
        }

        [HttpPost]
        public IActionResult MemberEdit(TMember member)
        {
            SetCategory();
            string uid = User.Identity.Name;
            var temp = _context.TMembers.FirstOrDefault(m => m.Uid == uid);

            if (ModelState.IsValid)
            {
                temp.Name = member.Name;
                temp.Pwd = member.Pwd;
                _context.SaveChanges();
            }
            return View(member);
        }


        public IActionResult CommentCreate(string productId)
        {
            SetCategory();
            var   VMProductComment = new VMProductComment(_context, productId);           
            return View(VMProductComment);
        }

        [HttpPost]
        public IActionResult CommentCreate(string ProductId, string Comment)
        {
            SetCategory();

            TComment comment = new TComment();  
            comment.PublishDate = DateTime.Now; 
            comment.ProductId = ProductId;
            comment.Comment = Comment;
            comment.IsReComment = "否";
            _context.TComments.Add(comment);    
            _context.SaveChanges(); 

            return RedirectToAction("CommentCreate", new { ProductId = comment.ProductId });
        }










    }
}
