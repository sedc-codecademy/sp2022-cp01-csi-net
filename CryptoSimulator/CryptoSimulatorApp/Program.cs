using CryptoSimulator.Common.Mappers;
using CryptoSimulator.Common.Models;
using CryptoSimulator.Configurations.DependencyInjection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuring Cors policy
builder.Services.AddCors(options => options.AddPolicy("myPolicy", policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

// Configuring AppSettings section
var appConfig = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appConfig);
// Using AppSettings
var appSettings = appConfig.Get<AppSettings>();
var secret = Encoding.ASCII.GetBytes(appSettings.Secret);

// Inject Dependecies
builder.Services
    .InjectAppDbContext(appSettings.ConnectionString)
    .RegisterFluentValidation()
    .RegisterAutoMapper()
    .RegisterServices()
    .RegisterRepositories()
    .AddJwtTokenConfiguration(secret)
    .AddSwaggerConfiguration();



var app = builder.Build();

app.UseCors("myPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();