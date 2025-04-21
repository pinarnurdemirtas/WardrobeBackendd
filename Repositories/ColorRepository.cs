using Microsoft.EntityFrameworkCore;
using WardrobeBackendd.Data;
using WardrobeBackendd.Model;

namespace WardrobeBackendd.Repositories
{
    public class ColorRepository : IColorRepository
    {
        private readonly WardrobeDbContext _context;

        public ColorRepository(WardrobeDbContext context)
        {
            _context = context;
        }

        // Tüm renkleri getirir
        public async Task<List<Color>> GetAllColorsAsync()
        {
            return await _context.Colors.ToListAsync();
        }
    }
}