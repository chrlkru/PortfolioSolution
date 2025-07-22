using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Shared
{
    public class Project
    {
        public int Id { get; set; }

        // Основные поля
        public string Slug { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        // Идентификация и SEO
        public string? ShortDescription { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public List<string> Tags { get; set; } = new();
        public string? Category { get; set; }

        // Контент и детали
        public List<string> Images { get; set; } = new();
        public string? FeaturedImageUrl { get; set; }
        public List<string> Technologies { get; set; } = new();
        public DateTime? CompletionDate { get; set; }
        public string? RepositoryUrl { get; set; }

        // Метрики и состояние
        public DateTime? UpdatedAt { get; set; }
        public int ViewCount { get; set; }
        public bool IsPublished { get; set; }
        public int Priority { get; set; }
        public bool IsFeatured { get; set; }
    }

    public class PortfolioDbContext : DbContext
    {
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options)
            : base(options) { }

        public DbSet<Project> Projects => Set<Project>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasIndex(p => p.Slug)
                .IsUnique();
            // Для полей-коллекций можно настроить ValueConverter или отдельную сущность
        }
    }
}
