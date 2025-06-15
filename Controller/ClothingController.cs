using Microsoft.AspNetCore.Mvc;
using WardrobeBackendd.Model;
using WardrobeBackendd.Services;

namespace WardrobeBackendd.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClothingController : ControllerBase
    {
        private readonly ClothingService _clothingService;

        public ClothingController(ClothingService clothingService)
        {
            _clothingService = clothingService;
        }

        // Resim yükler ve URL döner
        [HttpPost("uploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            try
            {
                var imageUrl = await _clothingService.UploadImageAsync(file, Request);
                if (string.IsNullOrEmpty(imageUrl))
                    return BadRequest(new { message = "Resim yüklenemedi." });

                return Ok(new { imageUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Hata: {ex.Message}" });
            }
        }

		// Yeni kıyafet ekler
		[HttpPost("newClothing")]
		public async Task<IActionResult> AddClothing([FromBody] Cloth cloth)
		{
			try
			{
				if (cloth == null || string.IsNullOrEmpty(cloth.Image_url))
					return BadRequest(new { message = "Eksik veri." });

				var clothing = new Clothes
				{
					Id = 0,
					User_id = cloth.User_id,
					Name = cloth.Name,
					Image_url = cloth.Image_url,
					Category_id = cloth.Category_id,
					Season_id = cloth.Season_id,
					ColorId = cloth.ColorId
				};

				var result = await _clothingService.AddClothingAsync(clothing);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = $"Hata: {ex.Message}" });
			}
		}


		// Belirli kullanıcıya ait kıyafetleri getirir
		[HttpGet("clothes/{userId}")]
        public async Task<IActionResult> GetClothesByUserId(int userId)
        {
            try
            {
                var clothes = await _clothingService.GetClothesByUserIdAsync(userId);
                if (clothes == null || clothes.Count == 0)
                    return NotFound(new { message = "Bu kullanıcıya ait kıyafet bulunamadı." });

                return Ok(clothes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Hata: {ex.Message}" });
            }
        }

        // Kıyafeti siler
        [HttpDelete("deleteClothing{id}")]
        public async Task<IActionResult> DeleteClothing(int id)
        {
            try
            {
                var result = await _clothingService.DeleteClothingAsync(id);
                if (!result)
                    return NotFound(new { message = "Kıyafet bulunamadı." });

                return Ok(new { message = "Kıyafet başarıyla silindi." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Hata: {ex.Message}" });
            }
        }

        // Kullanıcının kıyafetlerini kategoriye göre gruplu getirir
        [HttpGet("clothes/grouped/{userId}")]
        public async Task<IActionResult> GetClothesGroupedByCategory(int userId)
        {
            try
            {
                var result = await _clothingService.GetClothesGroupedByCategoryAsync(userId);
                if (result == null || result.Count == 0)
                    return NotFound(new { message = "Hiç kıyafet bulunamadı." });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Hata: {ex.Message}" });
            }
        }

		[HttpGet("clothes/grouped-by-parent/{userId}")]
		public async Task<IActionResult> GetClothesGroupedByParentCategory(int userId)
		{
			try
			{
				var result = await _clothingService.GetClothesGroupedByParentCategoryAsync(userId);
				if (result == null || result.Count == 0)
					return NotFound(new { message = "Hiç kıyafet bulunamadı." });

				return Ok(result);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = $"Hata: {ex.Message}" });
			}
		}

	}
}
