using Microsoft.EntityFrameworkCore;
using CrawlerProventos.Core.Models;

namespace CrawlerProventos.Infrastructure.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {            
        }

        public DbSet<Provento> Proventos { get; set; }
        public DbSet<CotacaoPorLoteMil> CotacaoPorLoteMil { get; set; }
        public DbSet<TipoAtivo> TipoAtivo { get; set; }
        public DbSet<TipoProvento> TipoProvento { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}