using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestApiTinderClone.Models;

namespace RestApiTinderClone.Data
{
    public class TinderDataContext : DbContext
    {
        public readonly IConfiguration _config;
        public DbSet<User> Users { get; set; }
        public DbSet<UserPhoto> UserPhotos { get; set; }

        public DbSet<Matches> Matches { get;set; }
        public TinderDataContext(IConfiguration config) {
            Users = Set<User>();
            UserPhotos = Set<UserPhoto>();
            Matches = Set<Matches>();
            _config = config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
            optionsBuilder.UseNpgsql("Server=postgres;Port=5432;Database=ZinderDB;User Id=postgres;Password=postgres;");
        }
    }
}
