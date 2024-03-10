using EntityLayer.WebApplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configuration.WebApplication
{
    public class TestimonalConfig : IEntityTypeConfiguration<Testimonal>
    {
        public void Configure(EntityTypeBuilder<Testimonal> builder)
        {
            builder.Property(x => x.CreatedDate).IsRequired().HasMaxLength(10);
            builder.Property(x => x.UpdatedDate).HasMaxLength(10);

            builder.Property(x => x.RowVersion).IsRowVersion();

            builder.Property(x => x.Comment).IsRequired().HasMaxLength(2000);
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            builder.Property(x => x.FileName).IsRequired();
            builder.Property(x => x.FileType).IsRequired();

            builder.HasData(new Testimonal
            {
                Id = 1,
                Comment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor",
                Title = "TestTitle",
                FullName = "John Wick",
                FileName = "test",
                FileType = "test",
				CreatedDate = "29/02/24",
			}, new Testimonal
            {
                Id = 2,
                Comment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor",
                Title = "TestTitle2",
                FullName = "Scot Adkins",
                FileName = "test2",
                FileType = "test2",
				CreatedDate = "29/02/24",
			}, new Testimonal
            {
                Id = 3,
                Comment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor",
                Title = "TestTitle3",
                FullName = "The Rock",
                FileName = "test3",
                FileType = "test3",
				CreatedDate = "29/02/24",
			}
            );
        }
    }
}
