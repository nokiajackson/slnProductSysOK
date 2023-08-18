using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using prjProductSys.Models;
using prjProductSys.ViewModels;

namespace prjProductSys.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        string _path;
        DbProductContext _context;
        public AdminController(DbProductContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _path = $@"{hostEnvironment.WebRootPath}\Images";
        }

        public IActionResult Index()
        {
            return View(_context.TCategories.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TCategory category)
        {
            if (ModelState.IsValid)
            {
                _context.TCategories.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Edit(int id)
        {
            var category = _context.TCategories.FirstOrDefault(m => m.CategoryId == id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(TCategory category)
        {
            if (ModelState.IsValid)
            {
                int id = category.CategoryId;
                var editCategory = _context.TCategories.FirstOrDefault(m => m.CategoryId == id);
                editCategory.CategoryName = category.CategoryName;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Delete(int id)
        {
            var category = _context.TCategories.FirstOrDefault(m => m.CategoryId == id);
            _context.TCategories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        //----------------------------------------

        void SetCategory()
        {
            ViewBag.Category = _context.TCategories.ToList();
        }


        public IActionResult BookCreate()
        {
            SetCategory();
            return View();  
        }


        [HttpPost]
        public async Task<IActionResult> BookCreate(TProduct product, IFormFile formFile)
        {
            SetCategory();
            if (ModelState.IsValid)
            {

                if (formFile != null)
                {
                    if (formFile.Length > 0)
                    {
                        string savePath = $@"{_path}\{formFile.FileName}";
                        using (var steam = new FileStream(savePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(steam);
                        }
                        product.Img = formFile.FileName;
                        _context.TProducts.Add(product);
                        _context.SaveChanges();
                        return RedirectToAction("BookList");
                    }

                }
            }
            return View(product);
        }


        public IActionResult BookList(int CategoryId=1)
        {

            var  categoryProducts = new VMCategoryProducts(_context, CategoryId);

            return View(categoryProducts);
        }

        public IActionResult CommentManager(string productId)
        {
            SetCategory();
            var VMProductComment = new VMProductComment(_context, productId);
            return View(VMProductComment);
        }

        [HttpPost]
        public IActionResult CommentManager(int Id, string ReComment)
        {
            SetCategory();

            var comment = _context.TComments.FirstOrDefault(c => c.Id == Id);
            
            comment.ReComment = ReComment;
            comment.IsReComment = "是";
            _context.SaveChanges();

            return RedirectToAction("CommentManager", new { ProductId = comment.ProductId });
        }

        public IActionResult MemberList()
        {
            var member = _context.TMembers.ToList();
            return View(member);
        }




    }
}
