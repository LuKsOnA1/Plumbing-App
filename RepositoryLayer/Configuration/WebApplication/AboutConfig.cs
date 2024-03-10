using EntityLayer.WebApplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configuration.WebApplication
{
    public class AboutConfig : IEntityTypeConfiguration<About>
    {
        public void Configure(EntityTypeBuilder<About> builder)
        {
            builder.Property(x => x.CreatedDate).IsRequired().HasMaxLength(10);
            builder.Property(x => x.UpdatedDate).HasMaxLength(10);
            builder.Property(x => x.RowVersion).IsRowVersion();
			builder.Property(x => x.FileName).IsRequired();
			builder.Property(x => x.FileType).IsRequired();

			builder.Property(x => x.Header).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(5000);
            builder.Property(x => x.Clients).IsRequired().HasMaxLength(5);
            builder.Property(x => x.Projects).IsRequired().HasMaxLength(5);
            builder.Property(x => x.HoursOfSupport).IsRequired().HasMaxLength(5);
            builder.Property(x => x.HardWorkers).IsRequired().HasMaxLength(5);


            // Seed data...
            builder.HasData(new About
            {
                Id = 1,
                CreatedDate = "29/02/24",
                Header = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, " +
                              "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                              "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris " +
                              "nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit " +
                              "in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
                Clients = 5,
                Projects = 5,
                HoursOfSupport = 150,
                HardWorkers = 3,
                FileName = "test",
                FileType = "test",
                SocialMediaId = 1,
            });

        }
    }
}
