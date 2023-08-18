using prjProductSys.Models;

namespace prjProductSys.ViewModels
{
    public class VMProductComment
    {
        DbProductContext _context;

        public List<TComment > comment { get; set; }    
        public TProduct product { get; set; }   
        public VMProductComment(DbProductContext context, string ProductId)
        {
            _context = context;
            product = _context.TProducts.FirstOrDefault(m => m.ProductId == ProductId);
            comment = _context.TComments.Where(m => m.ProductId == ProductId).OrderByDescending(m=>m.Id).ToList(); 
        }
    }
}
