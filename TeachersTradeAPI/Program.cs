using System.Reflection;
using Microsoft.OpenApi.Models;
using TeachersTradeAPI.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();

builder.WebHost.UseUrls(configuration.GetSection("ApplicationUrls").Value ?? "http://localhost:5000");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "TeachersTradeAPI",
        Description = "ASP.NET Core Web of the TeachersTradeProject",
        Contact = new OpenApiContact { Name = "Dylariz", Url = new Uri("https://github.com/Dylariz") },
        License = new OpenApiLicense
            { Name = "MIT License", Url = new Uri("https://github.com/Dylariz/TeachersTrade/blob/master/LICENSE") }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddDbContext<ApplicationContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();