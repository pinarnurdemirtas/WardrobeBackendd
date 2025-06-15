using Microsoft.EntityFrameworkCore;
using WardrobeBackendd.Data;
using WardrobeBackendd.Model;

namespace WardrobeBackendd.Services
{
	public class RecommendationService
	{
		private readonly WardrobeDbContext _context;
		private readonly WeatherService _weatherService;
		private readonly ColorService _colorService;

		public RecommendationService(WardrobeDbContext context)
		{
			_context = context;
			_weatherService = new WeatherService();
			_colorService = new ColorService();
		}

		public RecommendationResult SuggestOutfits(int userId, double temperature)
		{
			var season = _weatherService.GetSeason(temperature);

			// Sezona uygun kullanıcının kıyafetleri
			var clothes = _context.Clothes
				.Include(c => c.Category)
				.Include(c => c.Season)
				.Include(c => c.Color)
				.Where(c => c.User_id == userId && c.Season.Name.ToLower() == season.ToLower())
				.ToList();

			// Ana kategoriler
			var topCategoryId = _context.Category.FirstOrDefault(c => c.Name == "Üst Giyim")?.Id;
			var bottomCategoryId = _context.Category.FirstOrDefault(c => c.Name == "Alt Giyim")?.Id;
			var shoeCategoryId = _context.Category.FirstOrDefault(c => c.Name == "Ayakkabı")?.Id;

			if (topCategoryId == null || bottomCategoryId == null || shoeCategoryId == null)
			{
				return new RecommendationResult
				{
					Season = season,
					Message = "Ana kategori ID'lerinden biri bulunamadı.",
					Outfits = new(),
					TotalClothesCount = clothes.Count
				};
			}

			var topCategoryIds = _context.Category
				.Where(c => c.ParentID == topCategoryId || c.Id == topCategoryId)
				.Select(c => c.Id)
				.ToList();

			var bottomCategoryIds = _context.Category
				.Where(c => c.ParentID == bottomCategoryId || c.Id == bottomCategoryId)
				.Select(c => c.Id)
				.ToList();

			var shoeCategoryIds = _context.Category
				.Where(c => c.ParentID == shoeCategoryId || c.Id == shoeCategoryId)
				.Select(c => c.Id)
				.ToList();

			var tops = clothes.Where(c => topCategoryIds.Contains(c.Category_id)).ToList();
			var bottoms = clothes.Where(c => bottomCategoryIds.Contains(c.Category_id)).ToList();
			var shoes = clothes.Where(c => shoeCategoryIds.Contains(c.Category_id)).ToList();

			var results = new List<(Clothes, Clothes, Clothes)>();

			foreach (var top in tops)
			{
				foreach (var bottom in bottoms)
				{
					if (!_colorService.AreColorsCompatible(top.Color.Name, bottom.Color.Name))
						continue;

					foreach (var shoe in shoes)
					{
						if (_colorService.AreColorsCompatible(bottom.Color.Name, shoe.Color.Name))
						{
							results.Add((top, bottom, shoe));
						}
					}
				}
			}

			return new RecommendationResult
			{
				Season = season,
				TotalClothesCount = clothes.Count,
				TopCount = tops.Count,
				BottomCount = bottoms.Count,
				ShoeCount = shoes.Count,
				TotalOutfits = results.Count,
				Outfits = results,
				Message = results.Count > 0 ? "Kombinler başarıyla oluşturuldu." : "Uygun kombin bulunamadı."
			};
		}

	}
}