using EntityLayer.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configuration.Identity
{
	public class AppUserRoleConfig : IEntityTypeConfiguration<AppUserRole>
	{

		public void Configure(EntityTypeBuilder<AppUserRole> builder)
		{
			builder.HasData(new AppUserRole
			{
				UserId = Guid.Parse("26CB59CA-EA6E-4B82-9791-AEE6BDD4C81D").ToString(),
				RoleId = Guid.Parse("7642BABD-0925-498C-9745-8DFC20BBABBA").ToString(),
			},
			new AppUserRole
			{
				UserId = Guid.Parse("94A83B4B-0637-429D-B10D-CD973C283FAC").ToString(),
				RoleId = Guid.Parse("9315C2D1-F8F6-488B-922F-729ACAFA5E24").ToString(),
			});
		}
	}
}
