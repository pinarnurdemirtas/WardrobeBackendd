using WardrobeBackendd.Model;
using WardrobeBackendd.Repositories;

namespace WardrobeBackendd.Services;

public class CombineService
{
    private readonly ICombineRepository _repository;

    public CombineService(ICombineRepository repository)
    {
        _repository = repository;
    }

    // Kıyafet ID'leri ile birlikte yeni kombin oluşturur
    public async Task<Combine> CreateCombineWithClothesAsync(CreateCombineDto dto)
    {
        var combine = new Combine
        {
            UserId = dto.UserId,
            Name = dto.Name,
            CreatedAt = DateTime.Now,
            IsFavorite = 0
        };

        var createdCombine = await _repository.CreateCombineAsync(combine);

        var combineClothes = dto.ClothIds.Select(id => new CombineClothes
        {
            CombineId = createdCombine.Id,
            ClothId = id
        }).ToList();

        await _repository.AddClothesToCombineAsync(combineClothes);

        return createdCombine;
    }

    // Kombini favorilere ekler
    public async Task MarkCombineAsFavoriteAsync(int combineId)
    {
        await _repository.MarkAsFavoriteAsync(combineId);
    }

    // Kullanıcının favori kombinlerini getirir
    public async Task<List<Combine>> GetFavoriteCombinesAsync(int userId)
    {
        return await _repository.GetFavoriteCombinesAsync(userId);
    }

    // Kombin favori durumunu değiştirir (aç/kapat)
    public async Task ToggleFavoriteAsync(int combineId)
    {
        var combine = await _repository.GetByIdAsync(combineId);
        if (combine == null)
            throw new Exception("Kombin bulunamadı");

        combine.IsFavorite = combine.IsFavorite == 1 ? 0 : 1;
        await _repository.UpdateAsync(combine);
    }

    // Kullanıcının tüm kombinlerini getirir
    public async Task<List<Combine>> GetCombinesByUserAsync(int userId)
    {
        return await _repository.GetCombinesByUserIdAsync(userId);
    }
}