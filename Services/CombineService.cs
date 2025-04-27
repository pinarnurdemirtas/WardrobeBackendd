using Microsoft.EntityFrameworkCore;
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

	public async Task<List<CombineWithClothesDto>> GetCombinesWithClothesByUserAsync(int userId)
	{
		var combines = await _repository.GetCombinesByUserIdAsync(userId);
		var combineDtos = new List<CombineWithClothesDto>();

		foreach (var combine in combines)
		{
			var combineClothes = await _repository.GetCombineClothesByCombineIdAsync(combine.Id);
			var clothesIds = combineClothes.Select(cc => cc.ClothId).ToList();

			var allClothes = await _repository.GetAllClothesAsync(); 
			var clothes = allClothes
				.Where(cl => clothesIds.Contains(cl.Id))
				.ToList();

			combineDtos.Add(new CombineWithClothesDto
			{
				Id = combine.Id,
				UserId = combine.UserId,
				Name = combine.Name,
				CreatedAt = combine.CreatedAt,
				IsFavorite = combine.IsFavorite,
				IsPublic = combine.IsPublic,
				Clothes = clothes
			});
		}

		return combineDtos;
	}

	public async Task<List<CombineWithClothesDto>> GetFavoriteCombinesWithClothesAsync(int userId)
	{
		var combines = await _repository.GetFavoriteCombinesAsync(userId);
		var combineDtos = new List<CombineWithClothesDto>();

		foreach (var combine in combines)
		{
			var combineClothes = await _repository.GetCombineClothesByCombineIdAsync(combine.Id);
			var clothesIds = combineClothes.Select(cc => cc.ClothId).ToList();

			var allClothes = await _repository.GetAllClothesAsync();
			var clothes = allClothes.Where(cl => clothesIds.Contains(cl.Id)).ToList();

			combineDtos.Add(new CombineWithClothesDto
			{
				Id = combine.Id,
				UserId = combine.UserId,
				Name = combine.Name,
				CreatedAt = combine.CreatedAt,
				IsFavorite = combine.IsFavorite,
				IsPublic = combine.IsPublic,
				Clothes = clothes
			});
		}

		return combineDtos;
	}




}