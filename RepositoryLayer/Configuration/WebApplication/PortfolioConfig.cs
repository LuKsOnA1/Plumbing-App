﻿using EntityLayer.WebApplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configuration.WebApplication
{
    public class PortfolioConfig : IEntityTypeConfiguration<Portfolio>
    {
        public void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder.Property(x => x.CreatedDate).IsRequired().HasMaxLength(10);
            builder.Property(x => x.UpdatedDate).HasMaxLength(10);
            builder.Property(x => x.RowVersion).IsRowVersion();

            // One Category can have many Portfolios
            builder.HasOne(x => x.Category).WithMany(x => x.Portfolios).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
            builder.Property(x => x.FileName).IsRequired();
            builder.Property(x => x.FileType).IsRequired();

            builder.HasData(new Portfolio
            {
                Id = 1,
                CategoryId = 1,
                FileName = "Test",
                FileType = "test",
                Title = "Test picture",
				CreatedDate = "29/02/24",
			}, new Portfolio
            {
                Id = 2,
                CategoryId = 1,
                FileName = "Test2",
                FileType = "test2",
                Title = "Test picture2",
				CreatedDate = "29/02/24",
			}, new Portfolio
            {
                Id = 3,
                CategoryId = 2,
                FileName = "Test3",
                FileType = "test3",
                Title = "Test picture3",
				CreatedDate = "29/02/24",
			}, new Portfolio
            {
                Id = 4,
                CategoryId = 2,
                FileName = "Test4",
                FileType = "test4",
                Title = "Test picture4",
				CreatedDate = "29/02/24",
			}
            );
        }
    }
}
