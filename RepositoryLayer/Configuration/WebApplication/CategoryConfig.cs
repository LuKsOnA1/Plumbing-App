﻿using EntityLayer.WebApplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configuration.WebApplication
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.CreatedDate).IsRequired().HasMaxLength(10);
            builder.Property(x => x.UpdatedDate).HasMaxLength(10);
            builder.Property(x => x.RowVersion).IsRowVersion();

            // One Portfolio can have only one Category
            builder.HasMany(x => x.Portfolios).WithOne(x => x.Category).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

            builder.HasData(new Category
            {
                Id = 1,
                Name = "Projects",
				CreatedDate = "29/02/24",
			}, new Category
            {
                Id = 2,
                Name = "SiteWorks",
				CreatedDate = "29/02/24",
			});
        }
    }
}
