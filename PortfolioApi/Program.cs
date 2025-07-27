// в Program.cs (src/PortfolioApi/Program.cs)
using Microsoft.EntityFrameworkCore;
using PortfolioApi.Data;

var builder = WebApplication.CreateBuilder(args);

// регистрация DbContext с SQLite
builder.Services.AddDbContext<PortfolioDbContext>(opts =>
    opts.UseSqlite("Data Source=site.db"));

var app = builder.Build();

// автоприменение миграций
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();
    db.Database.Migrate();
}

app.Run();
