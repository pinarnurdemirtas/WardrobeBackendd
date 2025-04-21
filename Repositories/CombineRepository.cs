using Microsoft.EntityFrameworkCore;
using WardrobeBackendd.Data;
using WardrobeBackendd.Model;

namespace WardrobeBackendd.Repositories
{
    public class CombineRepository : ICombineRepository
    {
        private readonly WardrobeDbContext _context;
    
        public CombineRepository(WardrobeDbContext context)
        {
            _context = context;
        }
    
        // Yeni kombin oluşturur
        public async Task<Combine> CreateCombineAsync(Combine combine)
        {
            _context.Combines.Add(combine);
            await _context.SaveChangesAsync();
            return combine;
        }
    
        // Kombine kıyafet listesi ekler
        public async Task AddClothesToCombineAsync(List<CombineClothes> combineClothesList)
        {
            _context.CombineClothes.AddRange(combineClothesList);
            await _context.SaveChangesAsync();
        }
    
        // Kombini favori olarak işaretler
        public async Task MarkAsFavoriteAsync(int combineId)
        {
            var combine = await _context.Combines.FindAsync(combineId);
            if (combine != null)
            {
                combine.IsFavorite = 1;
                await _context.SaveChangesAsync();
            }
        }
    
        // Kullanıcının favori kombinlerini getirir
        public async Task<List<Combine>> GetFavoriteCombinesAsync(int userId)
        {
            return await _context.Combines
                .Where(c => c.UserId == userId && c.IsFavorite == 1)
                .ToListAsync();
        }
    
        // Kullanıcının tüm kombinlerini getirir
        public async Task<List<Combine>> GetCombinesByUserIdAsync(int userId)
        {
            return await _context.Combines
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }
    
        // ID ile kombin getirir
        public async Task<Combine?> GetByIdAsync(int id)
        {
            return await _context.Combines.FindAsync(id);
        }
    
        // Kombini günceller
        public async Task UpdateAsync(Combine combine)
        {
            _context.Combines.Update(combine);
            await _context.SaveChangesAsync();
        }
    }
}

