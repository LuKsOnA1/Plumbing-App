using EntityLayer.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configuration.Identity
{
    public class AppUserClaimConfig : IEntityTypeConfiguration<AppUserClaim>
    {
        public void Configure(EntityTypeBuilder<AppUserClaim> builder)
        {
            builder.HasData(new AppUserClaim
            {
                Id = 1,
                UserId = Guid.Parse("94A83B4B-0637-429D-B10D-CD973C283FAC").ToString(),
                ClaimType = "AdminObserverExpireDate",
                ClaimValue = "01/03/2024"
            });
        }
    }
}
