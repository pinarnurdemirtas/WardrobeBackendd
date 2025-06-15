using WardrobeBackendd.Model;
using WardrobeBackendd.Repositories;

namespace WardrobeBackendd.Services;

public class ClothingService
{
	private readonly IClothingRepository _repository;

	public ClothingService(IClothingRepository repository)
	{
		_repository = repository;
	}

	// Resim yükleyip URL'sini döner
	public async Task<string> UploadImageAsync(IFormFile file, HttpRequest request)
	{
		if (file == null || file.Length == 0)
			return null;

		var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
		if (!Directory.Exists(uploadsFolder))
			Directory.CreateDirectory(uploadsFolder);

		var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
		var filePath = Path.Combine(uploadsFolder, uniqueFileName);

		using (var stream = new FileStream(filePath, FileMode.Create))
		{
			await file.CopyToAsync(stream);
		}

		var imageUrl = $"{request.Scheme}://{request.Host}/uploads/{uniqueFileName}";
		return imageUrl;
	}

	// Yeni kıyafet ekler
	public async Task<Clothes> AddClothingAsync(Clothes clothing)
	{
		return await _repository.AddClothingAsync(clothing);
	}

	// Kullanıcının kıyafetlerini getirir
	public async Task<List<Clothes>> GetClothesByUserIdAsync(int userId)
	{
		return await _repository.GetClothesByUserIdAsync(userId);
	}

	// Kıyafeti siler (varsa resmi de siler)
	public async Task<bool> DeleteClothingAsync(int id)
	{
		var clothing = await _repository.GetByIdAsync(id);
		if (clothing == null)
			return false;

		if (!string.IsNullOrEmpty(clothing.Image_url))
		{
			var fileName = Path.GetFileName(new Uri(clothing.Image_url).LocalPath);
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);
			if (File.Exists(filePath))
				File.Delete(filePath);
		}

		return await _repository.DeleteClothingAsync(clothing);
	}

	// Kıyafetleri kategoriye göre gruplar
	public async Task<List<CategoryClothesDto>> GetClothesGroupedByCategoryAsync(int userId)
	{
		return await _repository.GetClothesGroupedByCategoryAsync(userId);
	}

	public async Task<List<ParentCategoryGroupDto>> GetClothesGroupedByParentCategoryAsync(int userId)
	{
		var clothes = await _repository.GetClothesByUserIdAsync(userId); // tüm kıyafetleri çek
		var categories = await _repository.GetAllCategoriesAsync(); // tüm kategorileri çek

		var result = clothes
			.Select(clothing => new
			{
				Clothing = clothing,
				Category = categories.FirstOrDefault(c => c.Id == clothing.Category_id)
			})
			.Where(x => x.Category != null)
			.GroupBy(x => x.Category.ParentID) // ParentId'ye göre grupla
			.Select(g => new ParentCategoryGroupDto
			{
				ParentCategoryId = g.Key ?? 0, // Eğer null ise 0 ata
				ParentCategoryName = categories.FirstOrDefault(c => c.Id == g.Key)?.Name ?? "Bilinmeyen",
				Clothes = g.Select(x => new ClothingDto
				{
					Id = x.Clothing.Id,
					UserId = x.Clothing.User_id,
					Name = x.Clothing.Name,
					ImageUrl = x.Clothing.Image_url,
					CategoryId = x.Clothing.Category_id,
					SeasonId = x.Clothing.Season_id,
					ColorId = x.Clothing.ColorId
				}).ToList()
			})
			.ToList();

		return result;
	}

	internal async Task AddClothingAsync(ClothingDto newClothing)
	{
		throw new NotImplementedException();
	}
}
