using BookingSystem.Core.Data;
using BookingSystem.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddEnvironmentVariables();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Missing Connection String");
builder.Services.AddDbContext<BookingDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", p =>
    {
        p.AllowAnyMethod();
        p.AllowAnyHeader();
        p.AllowAnyOrigin();
    });
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "BookingSystem.Api", Version = "v1" });
});

builder.Services.AddControllers();

builder.Services.AddScoped<BookingService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "FribergApi v1");
        options.RoutePrefix = string.Empty;
        options.EnablePersistAuthorization();
    });
    app.UseCors("AllowAll");
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
