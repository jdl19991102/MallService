using Orders.Domain.Command;

var builder = WebApplication.CreateBuilder(args);
//Logs Config
builder.AddLogsConfiguration();
try
{
    // Add services to the container.

    //Filters Config
    builder.Services.AddFiltersConfiguration();

    builder.Services.AddEndpointsApiExplorer();

    //Swagger Config
    builder.Services.AddSwaggerGen();

    //DbContexts Settings
    builder.Services.AddDatabaseConfiguration(builder.Configuration);

    // AutoMapper Settings
    builder.Services.AddAutoMapperConfiguration();

    // MediatR Settings
    builder.Services.AddMediatR(cfg => {
        cfg.RegisterServicesFromAssembly(typeof(AddNewOrderCommand).Assembly);
    });

    Console.WriteLine(typeof(Program).Assembly);
    Console.WriteLine(typeof(AddNewOrderCommand).Assembly);

    // .NET Native DI Abstraction
    builder.Services.AddDependencyInjectionConfiguration();

    // Test Db
    //builder.Services.Preload();
    TestDbConfiguration.Preload();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Serilog Middleware
    app.UseSerilogRequestLogging();

    // Exception Middleware
    app.UseMiddleware<ExceptionHandlingMiddleware>();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    // Ensure any buffered events are sent at shutdown
    Log.CloseAndFlush();
}


