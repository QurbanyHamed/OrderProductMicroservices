using Microsoft.EntityFrameworkCore;
using ProductService.Context;
using Serilog;

Log.Logger = new LoggerConfiguration()
	.WriteTo.Console()
	.CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

// Add Serilog
builder.Host.UseSerilog((ctx, lc) => lc
	.WriteTo.Console()
	.ReadFrom.Configuration(ctx.Configuration));


// Add services to the container.
builder.Services.AddDbContext<ProductDbContext>(opt =>
	opt.UseInMemoryDatabase("ProductDb"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
