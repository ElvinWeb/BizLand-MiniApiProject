using ApiProject.Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Core.Configurations
{
    public class FeatureConfiguration : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
           builder.Property(prop => prop.Title).IsRequired().HasMaxLength(30);
           builder.Property(prop => prop.Description).IsRequired().HasMaxLength(150);
           builder.Property(prop => prop.Icon).IsRequired().HasMaxLength(30);
        }
    }
}
