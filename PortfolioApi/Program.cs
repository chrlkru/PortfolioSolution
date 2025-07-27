// � Program.cs (src/PortfolioApi/Program.cs)
using Microsoft.EntityFrameworkCore;
using PortfolioApi.Data;

var builder = WebApplication.CreateBuilder(args);

// ����������� DbContext � SQLite
builder.Services.AddDbContext<PortfolioDbContext>(opts =>
    opts.UseSqlite("Data Source=site.db"));

var app = builder.Build();

// �������������� ��������
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();
    db.Database.Migrate();
}

app.Run();
