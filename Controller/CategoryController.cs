using Microsoft.AspNetCore.Mvc;
using WardrobeBackendd.Repositories;

namespace WardrobeBackendd.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // Ana kategorileri getirir
        [HttpGet("maincategories")]
        public IActionResult GetMainCategories()
        {
            try
            {
                var mainCategories = _categoryRepository.GetMainCategories();
                return Ok(mainCategories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ana kategoriler alınamadı.", detay = ex.Message });
            }
        }
        
		// Alt kategorileri cinsiyete göre getirir
		[HttpGet("subcategories/{parentId}/gender/{gender}")]
		public IActionResult GetSubCategoriesByGender(int parentId, string gender)
		{
			try
			{
				var subCategories = _categoryRepository.GetSubCategoriesByGender(parentId, gender);
				return Ok(subCategories);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Alt kategoriler alınamadı.", detay = ex.Message });
			}
		}

	}
}