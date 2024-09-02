using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Party_Management.Models;

namespace Party_Management.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceDetails> InvoicesDetails { get; set; }
        public virtual DbSet<Party> Parties { get; set; }
        public virtual DbSet<PartyAssignment> PartyAssignments { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductRate> ProductRates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ProductRate>()
            .Property(p => p.Rate)
            .HasColumnType("decimal(18,2)");

            builder.Entity<Invoice>().ToTable(nameof(Invoice));
            builder.Entity<InvoiceDetails>().ToTable(nameof(InvoiceDetails));
            builder.Entity<PartyAssignment>().ToTable(nameof(PartyAssignment));
            builder.Entity<Party>().ToTable(nameof(Party));
            builder.Entity<Product>().ToTable(nameof(Product));
            builder.Entity<ProductRate>().ToTable(nameof(ProductRate));

        }
    }
}
