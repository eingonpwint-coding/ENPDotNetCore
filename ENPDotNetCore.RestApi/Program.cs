using ENPDotNetCore.RestApi.Db;
using ENPDotNetCore.Shared;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("DbConnection2")!;
//builder.Services.AddScoped<AdoDotNetService>(n => new AdoDotNetService("connectionString"));
//n mean new instance
//builder.Services.AddScoped<DapperService>(n => new DapperService("connectionString"));
builder.Services.AddDbContext<AppDBContext>(opt =>
{
    opt.UseSqlServer(connectionString);
},ServiceLifetime.Transient,
ServiceLifetime.Transient);

builder.Services.AddScoped(n => new AdoDotNetService("connectionString"));
//n mean new instance
builder.Services.AddScoped(n => new DapperService("connectionString"));


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
