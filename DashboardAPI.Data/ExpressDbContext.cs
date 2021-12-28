using DashboardAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace DashboardAPI.Context
{
    public class ExpressDbContext : DbContext
    {
        public ExpressDbContext(DbContextOptions<ExpressDbContext> options)
        : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Thoughts> Thoughts { get; set; }
        public DbSet<ThoughtLikes> ThoughtLikes { get; set; }
        public DbSet<Feelings> Feelings { get; set; }
        public DbSet<CompanyFeelings> CompanyFeelings { get; set; }
        public DbSet<CompanyLinks> CompanyLinks { get; set; }
    }
}