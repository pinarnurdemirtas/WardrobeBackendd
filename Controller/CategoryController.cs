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

        // Belirli bir ana kategoriye ait alt kategorileri getirir
        [HttpGet("subcategories/{parentId}")]
        public IActionResult GetSubCategories(int parentId)
        {
            try
            {
                var subCategories = _categoryRepository.GetSubCategories(parentId);
                return Ok(subCategories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Alt kategoriler alınamadı.", detay = ex.Message });
            }
        }
    }
}