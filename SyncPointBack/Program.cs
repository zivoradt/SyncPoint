using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore;
using Serilog;
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

var app = builder.Build();

await app.UseDatabaseConnection();

app.UseRouting();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Docker"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Map controllers
});
app.UseHttpsRedirection();

app.UseMiddleware<RequestLogContextMiddleware>();
app.UseSerilogRequestLogging();

app.Run();