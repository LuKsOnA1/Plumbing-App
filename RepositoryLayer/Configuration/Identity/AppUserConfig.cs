using EntityLayer.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configuration.Identity
{
	public class AppUserConfig : IEntityTypeConfiguration<AppUser>
	{
		public void Configure(EntityTypeBuilder<AppUser> builder)
		{
			var admin = new AppUser
			{
				Id = Guid.Parse("26CB59CA-EA6E-4B82-9791-AEE6BDD4C81D").ToString(),
				Email = "Caciashvili.luka1@gmail.com",
				NormalizedEmail = "CACIASHVILI.LUKA1@GMAIL.COM",
				UserName = "TestAdmin",
				NormalizedUserName = "TESTADMIN",
				ConcurrencyStamp = Guid.NewGuid().ToString(),
				SecurityStamp = Guid.NewGuid().ToString(),

			};

			var adminPasswordHash = PasswordHash(admin, "TESTtest123@");
			admin.PasswordHash = adminPasswordHash;
			builder.HasData(admin);

			var member = new AppUser
			{
				Id = Guid.Parse("94A83B4B-0637-429D-B10D-CD973C283FAC").ToString(),
				Email = "Test.test@gmail.com",
				NormalizedEmail = "TEST.TEST@GMAIL.COM",
				UserName = "TestMember",
				NormalizedUserName = "TESTMEMBER",
				ConcurrencyStamp = Guid.NewGuid().ToString(),
				SecurityStamp = Guid.NewGuid().ToString(),

			};

			var memberPasswordHash = PasswordHash(member, "TESTtest123@");
			member.PasswordHash = memberPasswordHash;
			builder.HasData(member);
		}

		private string PasswordHash(AppUser user, string password)
		{
			var passwordHasher = new PasswordHasher<AppUser>();
			return passwordHasher.HashPassword(user, password);
		}
	}
}
