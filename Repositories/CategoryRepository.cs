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

		// Cinsiyete g�re alt kategorileri getirir
		public IEnumerable<Category> GetSubCategoriesByGender(int parentId, string gender)
		{
			return _context.Category
				.Where(c => c.ParentID == parentId && (c.Gender == gender || c.Gender == "Unisex"))
				.ToList();
		}

	}
}
