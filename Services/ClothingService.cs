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
}
