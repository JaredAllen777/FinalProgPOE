using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace ContractPoe.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<LecturerClaim> LecturerClaims { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
    }
}