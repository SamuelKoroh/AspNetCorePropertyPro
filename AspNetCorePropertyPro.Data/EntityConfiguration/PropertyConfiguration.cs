using AspNetCorePropertyPro.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCorePropertyPro.Data.EntityConfiguration
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Title).IsRequired().HasMaxLength(150);

            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18, 2)");

            builder.Property(x => x.State).IsRequired().HasMaxLength(100);

            builder.Property(x => x.City).IsRequired().HasMaxLength(100);

            builder.Property(x => x.Address).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Status).IsRequired().HasMaxLength(15);
            builder.Property(x => x.IsActive).IsRequired();

            builder.Property(x => x.Description).HasMaxLength(400);

            builder.HasOne(x => x.DealType).WithMany(x => x.Properties)
                .HasForeignKey(x => x.DealTypeId);

            builder.HasOne(x => x.Type).WithMany(x => x.Properties)
                .HasForeignKey(x => x.TypeId);
            builder.HasOne(x => x.Owner).WithMany(x => x.Properties).HasForeignKey(x => x.OwnerId);

            builder.ToTable("Properties");
        }
    }
}
