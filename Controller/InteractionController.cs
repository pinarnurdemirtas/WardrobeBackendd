using Microsoft.AspNetCore.Mvc;
using WardrobeBackendd.Model;
using WardrobeBackendd.Services;

namespace WardrobeBackendd.Controller;

[ApiController]
[Route("api/[controller]")]
public class InteractionController : ControllerBase
{
    private readonly InteractionService _interactionService;

    public InteractionController(InteractionService interactionService)
    {
        _interactionService = interactionService;
    }

    // Kombini keşfete gönderir (yayınlar)
    [HttpPut("publish/{combineId}")]
    public async Task<IActionResult> PublishCombine(int combineId)
    {
        try
        {
            var success = await _interactionService.PublishCombineAsync(combineId);
            if (!success)
                return NotFound("Kombin bulunamadı.");

            return Ok(new { message = "Kombin keşfete atıldı." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Yayınlama sırasında hata oluştu.", detay = ex.Message });
        }
    }

    // Kombin görünürlüğünü değiştirir (yayın/gizli)
    [HttpPut("toggle-visibility/{combineId}")]
    public async Task<IActionResult> ToggleCombineVisibility(int combineId)
    {
        try
        {
            var success = await _interactionService.ToggleCombineVisibilityAsync(combineId);
            if (!success)
                return NotFound("Kombin bulunamadı.");

            return Ok(new { message = "Kombin görünürlüğü değiştirildi." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Görünürlük değiştirme sırasında hata oluştu.", detay = ex.Message });
        }
    }

	// Yayınlanmış kombinleri getirir (keşfet sayfası)
	[HttpGet("explore")]
	public async Task<IActionResult> GetExploreCombines()
	{
		try
		{
			var combines = await _interactionService.GetPublicCombinesWithClothesAsync(); // 👈 DTO'lu versiyon
			return Ok(combines);
		}
		catch (Exception ex)
		{
			return StatusCode(500, new { message = "Keşfet kombinleri alınamadı.", detay = ex.Message });
		}
	}


	// Kombini beğenir
	[HttpPost("like/{combineId}/{userId}")]
    public async Task<IActionResult> LikeCombine(int combineId, int userId)
    {
        try
        {
            await _interactionService.LikeCombineAsync(userId, combineId);
            return Ok(new { message = "Beğeni kaydedildi." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Beğeni işlemi sırasında hata oluştu.", detay = ex.Message });
        }
    }

    // Kombinin beğeni sayısını getirir
    [HttpGet("like-count/{combineId}")]
    public async Task<IActionResult> GetLikeCount(int combineId)
    {
        try
        {
            var count = await _interactionService.GetLikeCountAsync(combineId);
            return Ok(new { likeCount = count });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Beğeni sayısı alınamadı.", detay = ex.Message });
        }
    }

    // Kombine yorum ekler
    [HttpPost("comment/{combineId}")]
    public async Task<IActionResult> AddComment(int combineId, [FromBody] AddCommentDto dto)
    {
        try
        {
            await _interactionService.AddCommentAsync(combineId, dto);
            return Ok(new { message = "Yorum kaydedildi." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Yorum ekleme sırasında hata oluştu.", detay = ex.Message });
        }
    }

    // Kombine ait yorumları getirir
    [HttpGet("comments/{combineId}")]
    public async Task<IActionResult> GetComments(int combineId)
    {
        try
        {
            var comments = await _interactionService.GetCommentsAsync(combineId);
            return Ok(comments);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Yorumlar alınamadı.", detay = ex.Message });
        }
    }
}
