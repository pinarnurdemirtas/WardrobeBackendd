using WardrobeBackendd.Model;

namespace WardrobeBackendd.Repositories
{
    public interface ICategoryRepository
    {
        // Ana kategorileri getirir (ParentID null olanlar)
        IEnumerable<Category> GetMainCategories(); 
        
        // Belirli bir kategoriye ait alt kategorileri getirir
        IEnumerable<Category> GetSubCategories(int parentId);
    }

}
