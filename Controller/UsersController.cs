using Microsoft.AspNetCore.Authorization;
using WardrobeBackendd.Model;
using WardrobeBackendd.Services;
using Microsoft.AspNetCore.Mvc;

namespace WardrobeBackendd.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        // Profil Güncelleme
        [HttpPut("update")]
        public async Task<IActionResult> UpdateProfile([FromBody] Users updatedUser)
        {
            try
            {
                var result = await _userService.UpdateUser(updatedUser);
                return result ? Ok("Profil güncellendi.") : BadRequest("Profil güncelleme başarısız.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Profil güncellenirken hata oluştu.", detay = ex.Message });
            }
        }

        // Hesap Silme
        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteAccount(string username)
        {
            try
            {
                var result = await _userService.DeleteUser(username);
                return result ? Ok("Hesap silindi.") : BadRequest("Hesap silme başarısız.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Hesap silinirken hata oluştu.", detay = ex.Message });
            }
        }
    }
}