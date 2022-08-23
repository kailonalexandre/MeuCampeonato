using MeuCampeonato.Data.Mapping;
using MeuCampeonato.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeuCampeonato.Data.Context
{
    public class MyContext : DbContext
    {
        public DbSet<MatchEntity> Games { get; set; }
        public DbSet<TeamsEntity> Teams { get; set; }
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MatchEntity>(new MatchsMap().Configure);
            modelBuilder.Entity<TeamsEntity>(new TeamsMap().Configure);
        }
    }
}
