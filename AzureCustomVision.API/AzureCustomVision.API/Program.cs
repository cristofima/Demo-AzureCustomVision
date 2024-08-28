using AzureCustomVision.API.Interfaces;
using AzureCustomVision.API.Options;
using AzureCustomVision.API.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// Add services to the container.

builder.Services.Configure<AzureVisionSettings>(
    builder.Configuration.GetSection("AzureVisionSettings"));

builder.Services.AddScoped<IVisualFeaturesParser, VisualFeaturesParser>();
builder.Services.AddScoped<IAzureCustomVisionService, AzureCustomVisionService>();
builder.Services.AddScoped<IAzureVisionService, AzureVisionService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var info = new OpenApiInfo()
{
    Title = "Azure Computer Vision",
    Version = "v1",
    Description = "Azure AI Vision and Azure Custom Vision",
    Contact = new OpenApiContact()
    {
        Name = "Cristopher Coronado"
    }
};

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", info);
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
