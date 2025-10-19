using Assessment.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMathService, MathService>();
builder.Services.AddScoped<IExternalCountryService, ExternalCountryService>();

builder.Services.AddHttpClient<IExternalCountryService, ExternalCountryService>(client => {
    client.BaseAddress = new Uri("https://restcountries.com/v3.1/");
});

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
