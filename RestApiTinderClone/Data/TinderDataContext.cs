using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestApiTinderClone.Configurations;
using RestApiTinderClone.Models;

namespace RestApiTinderClone.Data
{
    public class TinderDataContext : DbContext
    {
        
        public DbSet<User> Users { get; set; }
        public DbSet<UserPhoto> UserPhotos { get; set; }
        public DbSet<Matches> Matches { get;set; }
        public TinderDataContext(DbContextOptions<TinderDataContext> options) 
            : base(options)
        {
        
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new UserPhotoConfig());
            modelBuilder.ApplyConfiguration(new MatchesConfig());
            modelBuilder.ApplyConfiguration(new PreferenciesConfig());
            base.OnModelCreating(modelBuilder);

        }
    }
}
