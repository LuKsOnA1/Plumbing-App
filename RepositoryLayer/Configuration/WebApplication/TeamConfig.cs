﻿using EntityLayer.WebApplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configuration.WebApplication
{
    public class TeamConfig : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.Property(x => x.CreatedDate).IsRequired().HasMaxLength(10);
            builder.Property(x => x.UpdatedDate).HasMaxLength(10);

            builder.Property(x => x.RowVersion).IsRowVersion();

            builder.Property(x => x.FullName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            builder.Property(x => x.FileName).IsRequired();
            builder.Property(x => x.FileType).IsRequired();

            builder.HasData(new Team
            {
                Id = 1,
                FullName = "John Black",
                Title = "Proffesor",
                Facebook = "Facebook",
                Instagram = "Instagram",
                FileName = "Test",
                FileType = "Test",
				CreatedDate = "29/02/24",

			});
        }
    }
}