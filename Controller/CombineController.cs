using Microsoft.AspNetCore.Mvc;
using WardrobeBackendd.Model;
using WardrobeBackendd.Services;

namespace WardrobeBackendd.Controller;

[ApiController]
[Route("api/[controller]")]
public class CombineController : ControllerBase
{
    private readonly CombineService _combineService;

    public CombineController(CombineService combineService)
    {
        _combineService = combineService;
    }

    // Yeni kombin oluşturur
    [HttpPost("create")]
    public async Task<IActionResult> CreateCombine([FromBody] CreateCombineDto dto)
    {
        try
        {
            var result = await _combineService.CreateCombineWithClothesAsync(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Kombin oluşturulurken hata oluştu.", detay = ex.Message });
        }
    }

    // Kombini favorilere ekler
    [HttpPut("{combineId}/favorite")]
    public async Task<IActionResult> MarkFavorite(int combineId)
    {
        try
        {
            await _combineService.MarkCombineAsFavoriteAsync(combineId);
            return Ok(new { message = "Kombin favorilere eklendi." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Favori ekleme işlemi sırasında hata oluştu.", detay = ex.Message });
        }
    }

    // Kombin favori durumunu değiştirir (aç/kapat)
    [HttpPut("{combineId}/toggle-favorite")]
    public async Task<IActionResult> ToggleFavorite(int combineId)
    {
        try
        {
            await _combineService.ToggleFavoriteAsync(combineId);
            return Ok(new { message = "Favori durumu değiştirildi." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Favori durumu değiştirilirken hata oluştu.", detay = ex.Message });
        }
    }

    // Kullanıcının favori kombinlerini getirir
    [HttpGet("favorites/{userId}")]
    public async Task<IActionResult> GetFavorites(int userId)
    {
        try
        {
            var result = await _combineService.GetFavoriteCombinesAsync(userId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Favori kombinler alınamadı.", detay = ex.Message });
        }
    }

    // Kullanıcının tüm kombinlerini getirir
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserCombines(int userId)
    {
        try
        {
            var result = await _combineService.GetCombinesByUserAsync(userId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Kullanıcının kombinleri alınamadı.", detay = ex.Message });
        }
    }
}
