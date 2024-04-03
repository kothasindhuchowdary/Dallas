using NASADataApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var allowedorigins = "_allowedorigins";
builder.Services.AddCors(options =>
{
  options.AddPolicy(name: allowedorigins, policy => { policy.WithOrigins(allowedorigins); });
});
builder.Services.AddHttpClient<INASAApi, NASAApi>(client =>
{
  client.BaseAddress = new Uri("https://api.nasa.gov/");
});
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
