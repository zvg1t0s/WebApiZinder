using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestApiTinderClone.Models;

namespace RestApiTinderClone.Configurations
{
    public class MatchesConfig : IEntityTypeConfiguration<Matches>
    {
        public void Configure(EntityTypeBuilder<Matches> builder) { 
            builder.HasKey(x => x.Id);
            

        }
    }
}
