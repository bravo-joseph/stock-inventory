using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using resful_project.models;

namespace resful_project.data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Portfolio>(x => x.HasKey(p => new { p.AppUserId, p.StockId }));
            modelBuilder.Entity<Portfolio>().HasOne(x => x.AppUser).WithMany(x => x.Portfolios).HasForeignKey(x => x.AppUserId);
            modelBuilder.Entity<Portfolio>().HasOne(x => x.Stock).WithMany(x => x.Portfolios).HasForeignKey(x => x.StockId);
            List<IdentityRole> roles = new List<IdentityRole>{
                new IdentityRole {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                 new IdentityRole {
                    Id = "2",
                    Name = "User",
                    NormalizedName = "USER"
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
