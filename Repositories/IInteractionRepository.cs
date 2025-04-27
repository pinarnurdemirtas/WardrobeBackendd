using WardrobeBackendd.Model;

namespace WardrobeBackendd.Repositories;

public interface IInteractionRepository
{
    // Kombini ID ile getirir (yayın/gizleme için)
    Task<Combine?> GetCombineByIdAsync(int combineId);

    // Kombini günceller (yayın/gizle)
    Task UpdateCombineAsync(Combine combine);

    // Yayınlanmış (public) kombinleri getirir
    Task<List<Combine>> GetPublicCombinesAsync();

    // Beğeni ekler
    Task AddLikeAsync(Like like);

    // Kombine ait toplam beğeni sayısını getirir
    Task<int> GetLikeCountAsync(int combineId);

    // Kullanıcı daha önce beğenmiş mi kontrol eder
    Task<bool> HasUserLikedAsync(int userId, int combineId);

    // Yorum ekler
    Task AddCommentAsync(Comment comment);

    // Kombine ait yorumları getirir
    Task<List<Comment>> GetCommentsByCombineIdAsync(int combineId);
	Task<List<CombineClothes>> GetCombineClothesByCombineIdAsync(int combineId);
	Task<List<Users>> GetAllUsersAsync();

	Task<List<Clothes>> GetAllClothesAsync();

}