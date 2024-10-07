using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RegistrySearch.BusinessService;
using RegistrySearch.BusinessService.Dtos;
using RegistrySearch.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ISearchRegistryService, SearchRegistryService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
IConfiguration _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
Console.Write(_configuration["ConnnectionStrings:RegistryDB"]);
builder.Services.AddDbContext<RegistryDbContext>(options =>
{
    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PeopleFinder;Trusted_Connection=True;ConnectRetryCount=0", builder =>
        {
            builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<CustomExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
