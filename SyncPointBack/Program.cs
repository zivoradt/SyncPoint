using Microsoft.EntityFrameworkCore;
using Serilog;
using SyncPointBack.Helper.ErrorHandler;
using SyncPointBack.Persistance;
using SyncPointBack.Services;
using SyncPointBack.Utils.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Added Excel Related Model Database
builder.Services.ConfigureService();
builder.Services.AddDbContext<SyncPointDb>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Added Authentification Realated Database
builder.Services.AddDbContext<AuthDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

builder.Services.AddJwtAuthentication(builder.Configuration);

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

app.UseExceptionHandler("/error"); // Use custom error handling endpoint for other environments

app.UseHttpsRedirection();

app.UseRouting(); // Ensure routing is configured before authorization

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseMiddleware<RequestLogContextMiddleware>();
app.UseSerilogRequestLogging();

app.Run();