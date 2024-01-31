using Driver.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Config Mongo
builder.Services.Configure<DriverConfigurationDb>(builder.Configuration.GetSection("MongoDbConfig"));
builder.Services.AddSingleton<DriverService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
