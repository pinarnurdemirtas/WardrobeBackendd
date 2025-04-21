using Microsoft.AspNetCore.Mvc;
using WardrobeBackendd.Repositories;

namespace WardrobeBackendd.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorRepository _colorRepository;

        public ColorController(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }

        // Tüm renkleri getirir
        [HttpGet]
        public async Task<IActionResult> GetAllColors()
        {
            try
            {
                var colors = await _colorRepository.GetAllColorsAsync();
                return Ok(colors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Renkler alınırken bir hata oluştu.", detay = ex.Message });
            }
        }
    }
}