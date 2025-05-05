using BudgetService.Data;
using BudgetService.Services;
using BudgetService.Services.Gateway;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// -------------------- Configuration --------------------
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BudgetService API",
        Version = "v1",
        Description = "API for automatic budget generation and management"
    });
});

// EF Core DbContext configuration
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString, sqlOpts =>
    {
        sqlOpts.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null);
    });
});

// -------------------- Dependency Injection --------------------
builder.Services.AddScoped<IBudgetGateway, BudgetGateway>();
builder.Services.AddScoped<IAccountGateway, AccountGateway>();
builder.Services.AddScoped<BudgetService.Services.BudgetService>();
builder.Services.AddScoped<ITransactionGateway, TransactionGateway>();
builder.Services.AddScoped<TransactionService>();

var app = builder.Build();

// -------------------- Middleware Pipeline --------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BudgetService API v1");
        c.RoutePrefix = string.Empty; // Serve Swagger UI at root
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();