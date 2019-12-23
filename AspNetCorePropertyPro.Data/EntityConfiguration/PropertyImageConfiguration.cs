using AspNetCorePropertyPro.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCorePropertyPro.Data.EntityConfiguration
{
    public class PropertyImageConfiguration : IEntityTypeConfiguration<PropertyImage>
    {
        public void Configure(EntityTypeBuilder<PropertyImage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Secure_Url).IsRequired();
            builder.Property(x => x.Url).IsRequired();
            builder.Property(x => x.Public_Id).IsRequired();

            builder.HasOne(x => x.Property).WithMany(x => x.PropertyImages)
                .HasForeignKey(x => x.PropertyId).OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("PropertyImages");
        }
    }
}
