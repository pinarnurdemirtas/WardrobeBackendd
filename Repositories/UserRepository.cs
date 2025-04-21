using Microsoft.EntityFrameworkCore;
using WardrobeBackendd.Model;
using WardrobeBackendd.Data;

namespace WardrobeBackendd.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WardrobeDbContext _context;

        public UserRepository(WardrobeDbContext context)
        {
            _context = context;
        }

        // Kullanıcıyı kullanıcı adına göre getirir
        public async Task<Users?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
        }

        // Kullanıcıyı kullanıcı adına göre getirir (alternatif)
        public async Task<Users?> GetByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
        }

        // Kullanıcıyı ID'ye göre getirir
        public async Task<Users?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        // Tüm kullanıcıları getirir
        public async Task<List<Users>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // Yeni kullanıcı ekler
        public async Task<bool> AddUserAsync(Users user)
        {
            await _context.Users.AddAsync(user);
            return await SaveChangesAsync();
        }

        // Kullanıcıyı günceller
        public async Task<bool> UpdateUserAsync(Users user)
        {
            _context.Users.Update(user);
            return await SaveChangesAsync();
        }

        // Kullanıcıyı kullanıcı adına göre siler
        public async Task<bool> DeleteUserAsync(string username)
        {
            var user = await GetUserByUsernameAsync(username);
            if (user == null) return false;

            _context.Users.Remove(user);
            return await SaveChangesAsync();
        }

        // Kullanıcıyı ID'ye göre siler
        public async Task<bool> RemoveUserAsync(int id)
        {
            var user = await GetUserByIdAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            return await SaveChangesAsync();
        }

        // Kullanıcıyı doğrular
        public async Task<bool> VerifyUserAsync(string username)
        {
            var user = await GetUserByUsernameAsync(username);
            if (user == null) return false;

            user.Is_verified = true;
            return await SaveChangesAsync();
        }

        // Veritabanına değişiklikleri kaydeder
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
