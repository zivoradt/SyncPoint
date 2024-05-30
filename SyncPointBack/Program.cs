using Microsoft.EntityFrameworkCore;
using Serilog;
using SyncPointBack.Helper.ErrorHandler;
using SyncPointBack.Persistance;
using SyncPointBack.Services;
using SyncPointBack.Utils.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureService();
builder.Services.AddDbContext<SyncPointDb>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Added Serilog
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers();

//Added Global Error Handler
builder.Services.AddExceptionHandler<GlobalErrorHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

await app.UseDatabaseConnection();

app.UseRouting();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Docker"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler();
}

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseMiddleware<RequestLogContextMiddleware>();
app.UseSerilogRequestLogging();

app.Run();