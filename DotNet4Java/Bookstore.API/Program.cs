using Bookstore.API.Configuration;
using Bookstore.Repository;
using Bookstore.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<AuthorsRepository>();
builder.Services.AddSingleton<AuthorService>();

builder.Services.AddScoped<ISerialNumber, SimpleSerialNumber>();
builder.Services.AddScoped<ISerialNumber, SerialNumber>();

builder.Services.RegisterDelegateDependency();
builder.Services.AddTransient<IDependencyFactory, DependencyFactory>();

//builder.Services.AddKeyedScoped<ISerialNumber, SerialNumber>("Simple");

builder.Services.AddTransient<IComplexSerialNumber, ComplexSerialNumber>();

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
