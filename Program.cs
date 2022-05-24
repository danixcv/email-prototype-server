using EmailPrototypeServer.Models;
using EmailPrototypeServer.Properties;
using EmailPrototypeServer.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<DatabaseProperties>(
    builder.Configuration.GetSection("Database")
);
builder.Services.AddCors(options =>
      {
        options.AddPolicy("CorsPolicy",
            builder => 
            builder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .WithExposedHeaders("content-disposition")
            .AllowAnyHeader()
            .AllowCredentials()
            .SetPreflightMaxAge(TimeSpan.FromSeconds(3600)));
      });

builder.Services.AddSingleton<EmailService>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
