using Domain.Common;
using Domain.Users.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Categories.Models
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<User> Users { get; set; }

        public static void Configure(ModelBuilder builder)
        {
            builder.Entity<Category>(e =>
            {
                e.HasIndex(p => p.Name)
                 .IsUnique();

                e.Property(p => p.Name)
                 .HasMaxLength(100)
                 .IsRequired();

                e.Property(p => p.Description)
                 .IsRequired();

                e.Property(p => p.CreatedAt)
                 .HasDefaultValueSql("current_timestamp");
            });
        }
    }
}
