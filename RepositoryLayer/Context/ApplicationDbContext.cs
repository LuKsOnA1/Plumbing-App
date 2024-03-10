using CoreLayer.BaseEntities;
using EntityLayer.Identity.Entities;
using EntityLayer.WebApplication.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace RepositoryLayer.Context
{
	public class ApplicationDbContext : IdentityDbContext<AppUser,AppRole,string>
	{
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public ApplicationDbContext()
        {
            
        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
		public DbSet<HomePage> HomePages { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Testimonal> Testimonals { get; set; }

        // Ovveriding to check every library and get configurations from them
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			base.OnModelCreating(modelBuilder);
		}

        // Ovveriding to get CreatedDate and UpdatedDate, also when we update section CreatedDate will be same
		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
            foreach (var item in ChangeTracker.Entries()) 
            {
                
                if(item.Entity is BaseEntity entity)
                {

                    switch (item.State)
                    {
                        case EntityState.Added:
                            entity.CreatedDate = DateTime.Now.ToString("d");
                            break;

                        case EntityState.Modified:
                            Entry(entity).Property( x => x.CreatedDate).IsModified = false;
                            entity.UpdatedDate = DateTime.Now.ToString("d");
                            break;

                        default:
                            break;

                    }

                }

            }


			return base.SaveChangesAsync(cancellationToken);
		}

	}
}
