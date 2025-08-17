using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestApiTinderClone.Models;

namespace RestApiTinderClone.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Photos).WithOne(z => z.Owner);

            
        }
    }
    public class UserPhotoConfig : IEntityTypeConfiguration<UserPhoto>
    {
        public void Configure(EntityTypeBuilder<UserPhoto> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Owner).WithMany(z => z.Photos);
            
        }
    }
    public class PreferenciesConfig : IEntityTypeConfiguration<Preferencies> { 
        public void Configure(EntityTypeBuilder<Preferencies> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithOne(z => z.Preferencies).HasForeignKey<Preferencies>(c => c.UserId);
        }
    }

}
