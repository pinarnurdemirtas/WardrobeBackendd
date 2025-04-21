using WardrobeBackendd.Model;

namespace WardrobeBackendd.Repositories
{
    public interface IUserRepository
    {
        // ID'ye göre kullanıcıyı getirir
        Task<Users?> GetUserByIdAsync(int id);

        // Kullanıcı adına göre getirir
        Task<Users?> GetUserByUsernameAsync(string username);

        // Kullanıcı adına göre getirir (alternatif)
        Task<Users?> GetByUsernameAsync(string username);

        // Tüm kullanıcıları getirir
        Task<List<Users>> GetAllUsersAsync();

        // Yeni kullanıcı ekler
        Task<bool> AddUserAsync(Users user);

        // Kullanıcıyı günceller
        Task<bool> UpdateUserAsync(Users user);

        // Kullanıcıyı kullanıcı adına göre siler
        Task<bool> DeleteUserAsync(string username);

        // Kullanıcıyı ID'ye göre siler
        Task<bool> RemoveUserAsync(int id);

        // Kullanıcıyı doğrular
        Task<bool> VerifyUserAsync(string username);

        // Değişiklikleri kaydeder
        Task<bool> SaveChangesAsync();
    }
}