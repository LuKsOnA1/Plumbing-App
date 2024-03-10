using EntityLayer.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configuration.Identity
{
	public class AppRoleConfig : IEntityTypeConfiguration<AppRole>
	{
		public void Configure(EntityTypeBuilder<AppRole> builder)
		{
			builder.HasData(new AppRole
			{
				Id = Guid.Parse("7642BABD-0925-498C-9745-8DFC20BBABBA").ToString(),
				Name = "SuperAdmin",
				NormalizedName = "SUPERADMIN",
				ConcurrencyStamp = Guid.NewGuid().ToString(),
			},
			new AppRole
			{
				Id = Guid.Parse("9315C2D1-F8F6-488B-922F-729ACAFA5E24").ToString(),
				Name = "Member",
				NormalizedName = "MEMBER",
				ConcurrencyStamp = Guid.NewGuid().ToString(),
			});

			
		}
	}
}
