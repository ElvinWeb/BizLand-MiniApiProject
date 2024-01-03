using ApiProject.Core.Configurations;
using ApiProject.Core.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Data.DataAccessLayer
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) { }

        public DbSet<Feature> Features { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<PortfolioImage> PortfolioImages { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FeatureConfiguration).Assembly);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
                BaseEntity entity = data.Entity;

                switch (data.State)
                {
                    case EntityState.Deleted:
                        entity.DeletedDate = DateTime.UtcNow.AddHours(4);
                        break;
                    case EntityState.Added:
                        entity.CreatedDate = DateTime.UtcNow.AddHours(4);
                        break;
                    case EntityState.Modified:
                        entity.UpdatedDate = DateTime.UtcNow.AddHours(4);
                        break;
                    default:
                        break;
                }
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
