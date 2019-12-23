using AspNetCorePropertyPro.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCorePropertyPro.Data.EntityConfiguration
{
    public class DealTypeConfiguration : IEntityTypeConfiguration<DealType>
    {
        public void Configure(EntityTypeBuilder<DealType> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

            builder.Property(x => x.Description).HasMaxLength(400);

            builder.ToTable("DealTypes");
        }
    }
}
