using WardrobeBackendd.Model;
using WardrobeBackendd.Services;
using Microsoft.AspNetCore.Mvc;

namespace WardrobeBackendd.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        // Kullanıcı giriş işlemi
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginUser)
        {
            try
            {
                var (success, message, token, user) = await _userService.LoginUser(loginUser);
                if (!success)
                    return Unauthorized(new { message });

                return Ok(new { Token = token, User = user });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Giriş işlemi sırasında bir hata oluştu.", detay = ex.Message });
            }
        }

        // Yeni kullanıcı kaydı
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Users newUser)
        {
            try
            {
                bool success = await _userService.RegisterUser(newUser);
                return success ? Ok("Kayıt başarılı.") : BadRequest("Kullanıcı adı kullanılıyor.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Kayıt işlemi sırasında bir hata oluştu.", detay = ex.Message });
            }
        }

        // Kullanıcıyı doğrulama
        [HttpGet("register/verify/{username}")]
        public async Task<IActionResult> VerifyUser(string username)
        {
            try
            {
                var result = await _userService.VerifyUser(username);
                return result ? Ok("Your account has been successfully verified.") : BadRequest("User verification failed.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Doğrulama işlemi sırasında bir hata oluştu.", detay = ex.Message });
            }
        }
    }
}
