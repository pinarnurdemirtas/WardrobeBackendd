using WardrobeBackendd.Model;

namespace WardrobeBackendd.Repositories;

public interface ICombineRepository
{
    // Yeni kombin oluşturur
    Task<Combine> CreateCombineAsync(Combine combine);

    // Kombine kıyafetleri ekler
    Task AddClothesToCombineAsync(List<CombineClothes> combineClothesList);

    // Kombini favori olarak işaretler
    Task MarkAsFavoriteAsync(int combineId);

    // Kullanıcının favori kombinlerini getirir
    Task<List<Combine>> GetFavoriteCombinesAsync(int userId);

    // ID ile kombin getirir
    Task<Combine?> GetByIdAsync(int id);

    // Kombini günceller
    Task UpdateAsync(Combine combine);

    // Kullanıcının tüm kombinlerini getirir
    Task<List<Combine>> GetCombinesByUserIdAsync(int userId);
	Task<List<CombineClothes>> GetCombineClothesByCombineIdAsync(int combineId);
	Task<List<Clothes>> GetClothesByIdsAsync(List<int> clothIds);
	Task<List<Clothes>> GetAllClothesAsync();


}