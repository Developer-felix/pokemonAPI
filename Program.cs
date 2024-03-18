using Microsoft.EntityFrameworkCore;
using PocumanAPI;
using PocumanAPI.Data;
using PocumanAPI.IRepository;
using PocumanAPI.Repository;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
               x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IPokemanRepository, PokemonRepository> ();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository> ();
builder.Services.AddScoped<ICountryRepository, CountryRepository> ();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository> ();
builder.Services.AddScoped<IReviewerRepository, ReviewerRepository> ();
builder.Services.AddScoped<IReviewRepository, ReviewRepository> ();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnectionString")));

builder.Services.AddTransient<Seed>(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
if(args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);
void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory.CreateScope())
    {
        var service  = scope.ServiceProvider.GetService<Seed>();
        service.SeedDataContext();
    }
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
