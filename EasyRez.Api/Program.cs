using EasyRez.Infrastructure;
using System.Text.Json;
using System.Text.Json.Serialization;
using MediatR;
using EasyRez.Application.Reservation.Common;
using EasyRez.Api.Workers; // <-- YENİ EKLENDİ (Worker'ın namespace'i)
using Microsoft.EntityFrameworkCore; // <-- YENİ EKLENDİ (UseSqlServer için)

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "EasyRez API",
        Version = "v1",
        Description = "EasyRez Reservation System API"
    });
});
builder.Services.AddProblemDetails();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add MediatR
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(ReservasionMapper).Assembly);
});

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(ReservasionMapper).Assembly);

// Add Infrastructure services
builder.Services.AddInfrastructure(builder.Configuration);


// ---------- YENİ EKLENECEK ALAN ----------

// 1. WorkerSettings'i appsettings.json'dan okumak için kaydet
builder.Services.AddHostedService<TaskSchedulerWorker>();

// 2. HttpClientFactory'yi ekle
builder.Services.AddHttpClient("EasyRezClient", client =>
{
    // Worker'ın kendi API'sine (localhost) erişmesi için 
    // Properties/launchSettings.json dosyanızdaki 'applicationUrl' adresini
    // buraya base address olarak ekleyebilirsiniz.
    // Örnek: client.BaseAddress = new Uri("https://localhost:7001");
});

// 3. Worker'ı Hosted Service olarak kaydet
builder.Services.AddHostedService<ExternalApiWorker>();

// ------------------------------------------


var app = builder.Build();

// AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); // <-- SİLİNDİ (Bu Npgsql içindi)

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
// Enable Swagger in all environments
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EasyRez API V1");
    c.RoutePrefix = string.Empty; // Set Swagger UI at root URL
});

// Use CORS
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();