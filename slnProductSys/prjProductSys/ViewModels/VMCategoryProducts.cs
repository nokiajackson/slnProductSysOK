using prjProductSys.Models;

namespace prjProductSys.ViewModels
{
    public class VMCategoryProducts
    {
        DbProductContext _context;
        public VMCategoryProducts(DbProductContext context,  int categoryId)
        {
            _context = context;
            categoryName = _context.TCategories.FirstOrDefault
                (x => x.CategoryId == categoryId).CategoryName;
            category = _context.TCategories.ToList();
            products = _context.TProducts.Where(x => x.CategoryId == categoryId).ToList();  

        }
        public List<TCategory> category { get; set; }
        public string categoryName;
        public List<TProduct> products { get; set; }    
    }


}
