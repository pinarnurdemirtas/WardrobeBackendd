using Microsoft.AspNetCore.Mvc;
using WardrobeBackendd.Model;
using WardrobeBackendd.Services;

namespace WardrobeBackendd.Controller
{
	[Route("api/[controller]")]
	[ApiController]
	public class OutfitController : ControllerBase
	{
		private readonly RecommendationService _recommendationService;

		public OutfitController(RecommendationService recommendationService)
		{
			_recommendationService = recommendationService;
		}

		// Detaylı sonuçları RecommendationResult objesiyle döner
		[HttpGet("recommend/{userId}/{temperature}")]
		public ActionResult<RecommendationResult> GetRecommendations(int userId, double temperature)
		{
			var result = _recommendationService.SuggestOutfits(userId, temperature);
			return Ok(result);
		}

		[HttpGet("suggest")]
		public IActionResult GetSuggestions([FromQuery] int userId, [FromQuery] double temperature)
		{
			try
			{
				var result = _recommendationService.SuggestOutfits(userId, temperature);

				if (result.Outfits == null || !result.Outfits.Any())
				{
					return NotFound("Uygun kombin bulunamadı.");
				}

				// Her kombin için model oluştur
				var suggestions = result.Outfits.Select((combo, index) => new SuggestedOutfitDto
				{
					Id = index + 1, // örnek ID
					UserId = userId,
					Name = "Otomatik Kombin",
					CreatedAt = DateTime.Now,
					IsFavorite = 0,
					IsPublic = 1,
					Clothes = new List<SuggestedClothingDto>
			{
				new SuggestedClothingDto
				{
					Id = combo.Top.Id,
					User_id = combo.Top.User_id,
					Name = combo.Top.Name,
					Image_url = combo.Top.Image_url,
					Category_id = combo.Top.Category_id,
					Season_id = combo.Top.Season_id,
					ColorId = combo.Top.ColorId
				},
				new SuggestedClothingDto
				{
					Id = combo.Bottom.Id,
					User_id = combo.Bottom.User_id,
					Name = combo.Bottom.Name,
					Image_url = combo.Bottom.Image_url,
					Category_id = combo.Bottom.Category_id,
					Season_id = combo.Bottom.Season_id,
					ColorId = combo.Bottom.ColorId
				},
				new SuggestedClothingDto
				{
					Id = combo.Shoe.Id,
					User_id = combo.Shoe.User_id,
					Name = combo.Shoe.Name,
					Image_url = combo.Shoe.Image_url,
					Category_id = combo.Shoe.Category_id,
					Season_id = combo.Shoe.Season_id,
					ColorId = combo.Shoe.ColorId
				}
			}
				}).ToList();

				return Ok(suggestions);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
			}
		}


	}
}
