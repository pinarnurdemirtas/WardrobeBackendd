using WardrobeBackendd.Model;
using WardrobeBackendd.Repositories;

namespace WardrobeBackendd.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly Security _security;
        private readonly EmailService _emailService;

        public UserService(IUserRepository userRepository, Security security, EmailService emailService)
        {
            _userRepository = userRepository;
            _security = security;
            _emailService = emailService;
        }

        // Giriş işlemi yapar
        public async Task<(bool Success, string Message, string Token, Users User)> LoginUser(LoginModel loginUser)
        {
            var user = await _userRepository.GetUserByUsernameAsync(loginUser.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginUser.Password, user.Password))
            {
                return (false, "Geçersiz kullanıcı adı veya şifre.", null, null);
            }

            if (!user.Is_verified)
            {
                return (false, "Hesabınız doğrulanmamış.", null, null);
            }

            var token = _security.CreateToken(user);
            return (true, "Giriş başarılı.", token, user);
        }

        // Yeni kullanıcı kaydeder ve doğrulama maili gönderir
        public async Task<bool> RegisterUser(Users newUser)
        {
            var existingUser = await _userRepository.GetUserByUsernameAsync(newUser.Username);
            if (existingUser != null)
                return false;

            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            newUser.Is_verified = false;

            bool result = await _userRepository.AddUserAsync(newUser);
            if (!result) return false;

            var verificationUrl = $"http://wardrobe.pinarnur.com/api/Auth/register/verify/{newUser.Username}";

            // Doğrulama maili gönder
            await _emailService.SendConfirmationEmail(newUser.Email, verificationUrl, newUser.Fullname);

            return true;
        }

        // Kullanıcıyı doğrular
        public async Task<bool> VerifyUser(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null || user.Is_verified) return false;

            user.Is_verified = true;
            bool result = await _userRepository.UpdateUserAsync(user);

            if (result)
                await _emailService.SendAccountVerifiedEmail(user.Email);

            return result;
        }

        // Kullanıcıyı günceller
        public async Task<bool> UpdateUser(Users updatedUser)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(updatedUser.Id);
            if (existingUser == null) return false;

            existingUser.Username = updatedUser.Username;
            existingUser.Fullname = updatedUser.Fullname;
            existingUser.City = updatedUser.City;
            existingUser.Email = updatedUser.Email;
            existingUser.Password = BCrypt.Net.BCrypt.HashPassword(updatedUser.Password);

            return await _userRepository.UpdateUserAsync(existingUser);
        }

        // Kullanıcıyı siler
        public async Task<bool> DeleteUser(string username)
        {
            return await _userRepository.DeleteUserAsync(username);
        }
    }
}
