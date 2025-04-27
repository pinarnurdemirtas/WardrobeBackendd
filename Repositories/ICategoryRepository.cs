using WardrobeBackendd.Model;

namespace WardrobeBackendd.Repositories
{
	public interface ICategoryRepository
	{
		// Ana kategorileri getirir (ParentID null olanlar)
		IEnumerable<Category> GetMainCategories();

		// Cinsiyete g�re alt kategorileri getirir
		IEnumerable<Category> GetSubCategoriesByGender(int parentId, string gender);
	}
}
