using Assessment.EF.Context;
using Assessment.EF.Repositories;
using Assessment.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMathService, MathService>();
builder.Services.AddScoped<IExternalCountryService, ExternalCountryService>();
builder.Services.AddScoped<CountryRepo>();

builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ICountryCache, MemoryCountryCache>();
builder.Services.AddHttpClient<IExternalCountryService, ExternalCountryService>(client => {
    client.BaseAddress = new Uri(builder.Configuration.GetSection("ExternalApis:RestCountriesBaseUrl").Value);
});


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AssessmentDbContext>(options =>
    options.UseSqlServer(connectionString));


var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
