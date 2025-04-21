using Microsoft.EntityFrameworkCore;
using WardrobeBackendd.Data;
using WardrobeBackendd.Model;

namespace WardrobeBackendd.Repositories;

public class InteractionRepository : IInteractionRepository
{
    private readonly WardrobeDbContext _context;

    public InteractionRepository(WardrobeDbContext context)
    {
        _context = context;
    }

    // ID'ye göre kombin getirir (yayın/gizleme için)
    public async Task<Combine?> GetCombineByIdAsync(int combineId)
    {
        return await _context.Combines.FindAsync(combineId);
    }

    // Kombini günceller
    public async Task UpdateCombineAsync(Combine combine)
    {
        _context.Combines.Update(combine);
        await _context.SaveChangesAsync();
    }

    // Yayınlanmış (public) kombinleri getirir
    public async Task<List<Combine>> GetPublicCombinesAsync()
    {
        return await _context.Combines
            .Where(c => c.IsPublic == 1)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    // Kombine beğeni ekler
    public async Task AddLikeAsync(Like like)
    {
        _context.Likes.Add(like);
        await _context.SaveChangesAsync();
    }

    // Kombinin toplam beğeni sayısını getirir
    public async Task<int> GetLikeCountAsync(int combineId)
    {
        return await _context.Likes.CountAsync(l => l.CombineId == combineId);
    }

    // Kullanıcının kombin beğenip beğenmediğini kontrol eder
    public async Task<bool> HasUserLikedAsync(int userId, int combineId)
    {
        return await _context.Likes.AnyAsync(l => l.UserId == userId && l.CombineId == combineId);
    }

    // Kombine yorum ekler
    public async Task AddCommentAsync(Comment comment)
    {
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
    }

    // Kombine ait yorumları getirir
    public async Task<List<Comment>> GetCommentsByCombineIdAsync(int combineId)
    {
        return await _context.Comments
            .Where(c => c.CombineId == combineId)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }
}
