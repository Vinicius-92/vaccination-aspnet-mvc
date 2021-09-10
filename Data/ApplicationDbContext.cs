using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Vaccination.Models;
using Microsoft.AspNetCore.Identity;

namespace Vaccination.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole {Id = "99", Name = "admin", NormalizedName = "ADMIN".ToUpper() });
            var hasher = new PasswordHasher<IdentityUser>();
            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "66",
                    UserName = "admin@admin.com",
                    NormalizedUserName = "ADMIN@ADMIN.COM",
                    PasswordHash = hasher.HashPassword(null, "Gftadmin#2021"),
                    EmailConfirmed = true,
                    PhoneNumber = "66669995",
                    PhoneNumberConfirmed = true,
                    AccessFailedCount = 0,
                    Email = "admin@admin.com",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    LockoutEnabled = true,
                    TwoFactorEnabled = false
                }
            );


            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "99", 
                    UserId = "66"
                }
            );
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<VaccinationRecord> VaccinationRecords { get; set; }
        public DbSet<VaccinationPoint> VaccinationPoints { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<VaccineBatch> VaccineBatches { get; set; }
    }
}
