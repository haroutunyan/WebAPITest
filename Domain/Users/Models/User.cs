using Domain.Categories.Models;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users.Models
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public static void Configure(ModelBuilder builder)
        {
            builder.Entity<User>(e =>
            {
                e.Property(s => s.Id)
                 .ValueGeneratedOnAdd();

                e.Property(p => p.FirstName)
                 .IsRequired();

                e.Property(p => p.LastName)
                 .IsRequired();

                e.Property(p => p.Age)
                 .IsRequired();

                e.HasOne(p => p.Category)
                 .WithMany(b => b.Users)
                 .OnDelete(DeleteBehavior.SetNull);

                e.Property(p => p.CreatedAt)
                 .HasDefaultValueSql("current_timestamp");
            });
        }
    }
}
