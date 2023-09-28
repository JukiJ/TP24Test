using Microsoft.EntityFrameworkCore;
using TP24.Core.Interfaces.Repositories;
using TP24.Core.Interfaces.Services;
using TP24.Data;
using TP24.Infrastructure.Repositories;
using TP24.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = builder.Configuration["ConnectionString"]?.ToString() ??
                throw new ArgumentException("Connection String is not configured");

builder.Services.AddDbContext<TP24DbContext>(options => options.UseNpgsql(connectionString)).AddScoped<DbContext, TP24DbContext>();
builder.Services.AddTransient<IReceivableRepository, ReceivableRepository>();
builder.Services.AddTransient<IReceivableService, ReceivableService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<TP24DbContext>();

    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/Error");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }