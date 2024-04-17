using Serilog;
using SyncPointBack.Services;
using SyncPointBack.Services.ExcelInitiation;
using SyncPointBack.Utils.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureService();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

//Added Serilog
app.UseRouting();
// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
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