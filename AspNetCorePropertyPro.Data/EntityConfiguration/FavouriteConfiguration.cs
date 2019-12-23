using AspNetCorePropertyPro.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCorePropertyPro.Data.EntityConfiguration
{
    public class FavouriteConfiguration : IEntityTypeConfiguration<Favourite>
    {
        public void Configure(EntityTypeBuilder<Favourite> builder)
        {
            builder.HasKey(x => new { x.UserId, x.PropertyId });

            builder.HasOne(x => x.User).WithMany(x => x.Favourites)
                .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Property).WithMany(x => x.Favourites)
                .HasForeignKey(x=>x.PropertyId).OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.ToTable("Favourites");
        }
    }
}
