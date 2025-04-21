using WardrobeBackendd.Model;

namespace WardrobeBackendd.Repositories
{
    public interface IClothingRepository
    {
        // Yeni kıyafet ekler
        Task<Clothes> AddClothingAsync(Clothes clothing);

        // Kullanıcının kıyafetlerini getirir
        Task<List<Clothes>> GetClothesByUserIdAsync(int userId);

        // ID ile kıyafet getirir
        Task<Clothes?> GetByIdAsync(int id);

        // Kıyafeti siler
        Task<bool> DeleteClothingAsync(Clothes clothing);

        // Kıyafetleri kategoriye göre gruplar
        Task<List<CategoryClothesDto>> GetClothesGroupedByCategoryAsync(int userId);
    }
}