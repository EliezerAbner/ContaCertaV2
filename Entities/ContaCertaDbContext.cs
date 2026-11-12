using Microsoft.EntityFrameworkCore;

namespace Entities;

public class ContaCertaDbContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Movimentacao> Movimentacoes { get; set; }
    
    public ContaCertaDbContext(DbContextOptions options) : base (options)
    {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Usuario>().ToTable("Usuarios");
        modelBuilder.Entity<Movimentacao>().ToTable("Movimentacoes");
    }
}