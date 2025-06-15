using WardrobeBackendd.Model;
using Microsoft.EntityFrameworkCore;

namespace WardrobeBackendd.Data
{
    public class WardrobeDbContext : DbContext
    {
        public WardrobeDbContext(DbContextOptions<WardrobeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Clothes> Clothes { get; set; }
        public DbSet<Combine> Combines { get; set; }
        public DbSet<CombineClothes> CombineClothes { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Season> Season { get; set; }



    }
}