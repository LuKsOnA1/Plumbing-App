using EntityLayer.WebApplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configuration.WebApplication
{
    public class HomePageConfig : IEntityTypeConfiguration<HomePage>
    {
        public void Configure(EntityTypeBuilder<HomePage> builder)
        {
            builder.Property(x => x.CreatedDate).IsRequired().HasMaxLength(10);
            builder.Property(x => x.UpdatedDate).HasMaxLength(10);

            builder.Property(x => x.RowVersion).IsRowVersion();

            builder.Property(x => x.Header).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(2000);
            builder.Property(x => x.VideoLink).IsRequired();

            builder.HasData(new HomePage
            {
                Id = 1,
                Header = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, " +
                              "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                              "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris " +
                              "nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in " +
                              "reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
                VideoLink = "Test Video Link",
				CreatedDate = "29/02/24",
			});
        }
    }
}
