using EntityLayer.WebApplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configuration.WebApplication
{
    public class ServicesConfig : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.Property(x => x.CreatedDate).IsRequired().HasMaxLength(10);
            builder.Property(x => x.UpdatedDate).HasMaxLength(10);

            builder.Property(x => x.RowVersion).IsRowVersion();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(2000);
            builder.Property(x => x.Icon).IsRequired().HasMaxLength(100);

            builder.HasData(new Service
            {
                Id = 1,
                Icon = "bi bi-service1",
                Name = "Service 1",
				CreatedDate = "29/02/24",
				Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, " +
                              "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                              "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris " +
                              "nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in " +
                              "reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",

            }, new Service
            {
                Id = 2,
                Icon = "bi bi-service",
                Name = "Service 2",
				CreatedDate = "29/02/24",
				Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, " +
                              "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                              "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris " +
                              "nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in " +
                              "reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",

            }, new Service
            {
                Id = 3,
                Icon = "bi bi-service3",
                Name = "Service 3",
				CreatedDate = "29/02/24",
				Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, " +
                              "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                              "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris " +
                              "nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in " +
                              "reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",

            }
            );
        }
    }
}
