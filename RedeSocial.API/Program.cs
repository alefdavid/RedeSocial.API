using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RedeSocial.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "RedeSocial.API",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Alef David, Caio Gaspar, Henrique Pereira, Wesley Silva",
            Url = new Uri("https://github.com/henriqueapereira/RedeSocial.API")
        }
    });
});

// Context
var connectionString = builder.Configuration.GetConnectionString("DBRedeSocial");
builder.Services.AddDbContext<RedeSocialDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();

// Authentication and Authorization
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure exception handling
app.UseExceptionHandler("/error");
app.Map("/error", (HttpContext context) =>
{
    context.Response.StatusCode = 500;
    return context.Response.WriteAsync("An error occurred while processing your request.");
});

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();