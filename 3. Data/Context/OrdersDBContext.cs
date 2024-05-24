using _3._Data.Models;
using Microsoft.EntityFrameworkCore;

namespace _3._Data.Context;

public class OrdersDBContext : DbContext
{
    public OrdersDBContext(){}
    public OrdersDBContext(DbContextOptions<OrdersDBContext> options) : base(options) {}
    public DbSet<Client> Clients { get; set; }
    public DbSet<Order> Orders { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 4, 0));
            optionsBuilder.UseMySql("Server=localhost,3306;Uid=root;pwd=Elefanterita#;Database=Orders",
                serverVersion);
        }
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<Client>().ToTable("Client");
        builder.Entity<Client>().HasKey(t => t.Id);
        builder.Entity<Client>().Property(t => t.Name).IsRequired();
        builder.Entity<Client>().Property(t => t.Name).HasMaxLength(10);
       

        
        builder.Entity<Order>().ToTable("ClientOrder");
        builder.Entity<Order>().HasKey(t => t.Id);
        builder.Entity<Order>().Property(t => t.Amount).IsRequired();
        builder.Entity<Client>().Property(t => t.Name).HasMaxLength(10);
    }
}