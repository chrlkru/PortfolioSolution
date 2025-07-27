// файл: src/PortfolioApi/Data/PortfolioDbContext.cs
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared;

namespace PortfolioApi.Data
{
    public class PortfolioDbContext : DbContext
    {
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options)
            : base(options) { }

        public DbSet<Project> Projects { get; set; } = null!;  // таблица проектов

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var dictConverter = new ValueConverter<Dictionary<string, string>?, string>(
       v => JsonSerializer.Serialize(v, null),
       v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, null)!
   );

            var dictComparer = new ValueComparer<Dictionary<string, string>?>(
                (d1, d2) => d1 == null && d2 == null
                              || (d1 != null && d2 != null && d1.SequenceEqual(d2)),
                d => d == null ? 0 : d.Aggregate(0, (h, kv) => HashCode.Combine(h, kv.GetHashCode())),
                d => d == null ? null : new Dictionary<string, string>(d)
            );

            modelBuilder.Entity<Project>()
                .Property(p => p.Metadata)
                .HasConversion(dictConverter, dictComparer)  // конвертер + компаратор вместе
                .HasColumnType("TEXT");

            modelBuilder.Entity<Project>()
                .HasIndex(p => p.Slug)
                .IsUnique();
        }
    }
}
