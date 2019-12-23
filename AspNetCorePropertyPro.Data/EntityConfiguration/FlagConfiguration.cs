using AspNetCorePropertyPro.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCorePropertyPro.Data.EntityConfiguration
{
    public class FlagConfiguration : IEntityTypeConfiguration<Flag>
    {
        public void Configure(EntityTypeBuilder<Flag> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(150);

            builder.Property(x => x.Email).IsRequired().HasMaxLength(40);

            builder.Property(x => x.Reason).IsRequired().HasMaxLength(400);

            builder.HasOne(x => x.Property).WithMany(x => x.Flags)
                .HasForeignKey(x => x.PropertyId).OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Flags");
        }
    }
}
