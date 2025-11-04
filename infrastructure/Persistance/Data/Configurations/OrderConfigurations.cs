using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Data.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(O => O.Subtotal).HasColumnType("decimal(8,2)");
            builder.HasMany(O => O.Items).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(O => O.DeliveryMethod).WithMany().HasForeignKey(O => O.DeliveryMethodId);
            builder.OwnsOne(O => O.Address);
        }
    }
}
