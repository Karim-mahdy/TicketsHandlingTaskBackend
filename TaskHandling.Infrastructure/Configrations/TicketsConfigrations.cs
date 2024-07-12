using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsHandling.Domain.Models;

namespace TicketsHandling.Persistence.Configrations
{
    public class TicketsConfigrations : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .UseIdentityColumn();

            builder.Property(t => t.CreationDateTime)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.PhoneNumber)
                .IsRequired()
                .HasMaxLength(11)
                .IsFixedLength(true);

            builder.Property(t => t.Governorate)
                .IsRequired();

            builder.Property(t => t.City)
                .IsRequired();

            builder.Property(t => t.District)
                .IsRequired();

            builder.Property(t => t.IsHandled)
                .HasDefaultValue(false);
        }
    }

}
