using WardrobeBackendd.Model;

namespace WardrobeBackendd.Repositories
{
    public interface IColorRepository
    {
        // Tüm renkleri getirir
        Task<List<Color>> GetAllColorsAsync();
    }
}