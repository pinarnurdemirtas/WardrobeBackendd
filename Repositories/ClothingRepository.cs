using Microsoft.EntityFrameworkCore;
using WardrobeBackendd.Data;
using WardrobeBackendd.Model;

namespace WardrobeBackendd.Repositories
{
    public class ClothingRepository : IClothingRepository
    {
        private readonly WardrobeDbContext _context;

        public ClothingRepository(WardrobeDbContext context)
        {
            _context = context;
        }

        // Yeni kıyafet ekler
        public async Task<Clothes> AddClothingAsync(Clothes clothing)
        {
            await _context.Clothes.AddAsync(clothing);
            await _context.SaveChangesAsync();
            return clothing;
        }

        // Kullanıcının kıyafetlerini getirir
        public async Task<List<Clothes>> GetClothesByUserIdAsync(int userId)
        {
            return await _context.Clothes
                .Where(c => c.User_id == userId)
                .ToListAsync();
        }

        // ID'ye göre kıyafet getirir
        public async Task<Clothes?> GetByIdAsync(int id)
        {
            return await _context.Clothes.FindAsync(id);
        }

        // Kıyafeti siler
        public async Task<bool> DeleteClothingAsync(Clothes clothing)
        {
            _context.Clothes.Remove(clothing);
            await _context.SaveChangesAsync();
            return true;
        }

        // Kıyafetleri kategoriye göre gruplar
        public async Task<List<CategoryClothesDto>> GetClothesGroupedByCategoryAsync(int userId)
        {
            var result = await (from c in _context.Clothes
                join cat in _context.Category on c.Category_id equals cat.Id
                where c.User_id == userId
                group c by new { cat.Id, cat.Name } into g
                select new CategoryClothesDto
                {
                    CategoryId = g.Key.Id,
                    CategoryName = g.Key.Name,
                    Clothes = g.ToList()
                }).ToListAsync();

            return result;
        }

		public async Task<List<Category>> GetAllCategoriesAsync()
		{
			return await _context.Category.ToListAsync();
		}

	}
}