using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Business
{
    public class SportsContext : DbContext
    {
        public DbSet<Bet> Bets { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=.\;Database=SportsBook;Trusted_Connection=True;MultipleActiveResultSets=true");

            //optionsBuilder.UseSqlServer(
            //    @"Server=.\;Database=SportsBook-prod;Trusted_Connection=True;MultipleActiveResultSets=true");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bet>().Property(f => f.BetId).ValueGeneratedOnAdd();
            base.OnModelCreating(modelBuilder);
        }
    }
}
