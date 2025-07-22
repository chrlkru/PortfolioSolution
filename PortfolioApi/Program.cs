using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Serilog;
using PortfolioApi.Data; // namespace, ��� ����� PortfolioDbContext � ������ Project

namespace PortfolioApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. ������������ Serilog �� appsettings.json
            builder.Host.UseSerilog((ctx, lc) =>
                lc.ReadFrom.Configuration(ctx.Configuration));

            // 2. ������ ������ �� ��������
            var adminPassword = builder.Configuration["AdminPassword"];

            // 3. ��������� DbContext (SQLite)
            builder.Services.AddDbContext<PortfolioDbContext>(opts =>
                opts.UseSqlite("Data Source=site.db"));

            // 4. Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // 5. ����-�������� ��� ������
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();
                db.Database.Migrate();
            }

            // 6. Swagger UI (������ � Development)
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // ��������������� ����� ��� �����������
            static bool IsAuthorized(HttpContext ctx, string pwd) =>
                ctx.Request.Headers.TryGetValue("X-Admin-Password", out var header)
                && header == pwd;

            // 7. GET: ��� �������
            app.MapGet("/api/projects", async (PortfolioDbContext db) =>
                await db.Projects
                    .OrderByDescending(p => p.CreatedAt)
                    .ToListAsync()
            );

            // 8. GET: ������ �� slug
            app.MapGet("/api/projects/{slug}", async (string slug, PortfolioDbContext db) =>
                await db.Projects.FirstOrDefaultAsync(p => p.Slug == slug)
                    is Project project
                    ? Results.Ok(project)
                    : Results.NotFound()
            );

            // 9. POST: ������� ������ (��������)
            app.MapPost("/api/projects", async (Project project, HttpContext ctx, PortfolioDbContext db) =>
            {
                if (!IsAuthorized(ctx, adminPassword))
                    return Results.Unauthorized();

                project.CreatedAt = DateTime.UtcNow;
                db.Projects.Add(project);
                await db.SaveChangesAsync();
                return Results.Created($"/api/projects/{project.Slug}", project);
            });

            // 10. PUT: �������� ������ �� id (��������)
            app.MapPut("/api/projects/{id:int}", async (int id, Project input, HttpContext ctx, PortfolioDbContext db) =>
            {
                if (!IsAuthorized(ctx, adminPassword))
                    return Results.Unauthorized();

                var project = await db.Projects.FindAsync(id);
                if (project is null) return Results.NotFound();

                project.Title = input.Title;
                project.Description = input.Description;
                project.Slug = input.Slug;
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            // 11. DELETE: ������� ������ �� id (��������)
            app.MapDelete("/api/projects/{id:int}", async (int id, HttpContext ctx, PortfolioDbContext db) =>
            {
                if (!IsAuthorized(ctx, adminPassword))
                    return Results.Unauthorized();

                var project = await db.Projects.FindAsync(id);
                if (project is null) return Results.NotFound();

                db.Projects.Remove(project);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            app.Run();
        }
    }
}
