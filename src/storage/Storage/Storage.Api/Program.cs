using Storage.Api.Configurations;
using Storage.Api.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddFiltersConfiguration();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//DbContexts Settings
builder.Services.AddDatabaseConfiguration(builder.Configuration);

builder.Services.AddDependencyInjection();

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
