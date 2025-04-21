using WardrobeBackendd.Data;
using WardrobeBackendd.Model;

namespace WardrobeBackendd.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly WardrobeDbContext _context;

        public CategoryRepository(WardrobeDbContext context)
        {
            _context = context;
        }
        
        // Ana kategorileri getirir (ParentID null olanlar)
        public IEnumerable<Category> GetMainCategories()
        {
            return _context.Category
                .Where(c => c.ParentID == null)
                .ToList();
        }
        
        // Belirli bir kategoriye ait alt kategorileri getirir
        public IEnumerable<Category> GetSubCategories(int parentId)
        {
            return _context.Category
                .Where(c => c.ParentID == parentId)
                .ToList();
        }
    }
}