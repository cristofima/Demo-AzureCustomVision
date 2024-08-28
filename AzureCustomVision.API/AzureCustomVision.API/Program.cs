using AzureCustomVision.API.Interfaces;
using AzureCustomVision.API.Options;
using AzureCustomVision.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<AzureVisionSettings>(
    builder.Configuration.GetSection("AzureVisionSettings"));

builder.Services.AddScoped<IAzureCustomVisionService, AzureCustomVisionService>();
builder.Services.AddScoped<IAzureVisionService, AzureVisionService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
