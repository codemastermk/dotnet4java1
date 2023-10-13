using Bookstore.API.Configuration;
using Bookstore.API.Filters;
using Bookstore.API.Middleware;
using Bookstore.Data;
using Bookstore.Models;
using Bookstore.Repository;
using Bookstore.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

var myPolicy = "myPolicy";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
options.Filters.Add(new ConsoleActionFilterAttribute("Program"))).AddJsonOptions(o => { o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });


builder.Services.AddDbContext<BookstoreContext>(options => options.UseSqlServer(builder.Configuration.GetValue<string>("ConnectionsStrings:DefaultConnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "DotNetForJava",
        Description = "Some Description",
        TermsOfService = new Uri("http://example.com"),
        Contact = new OpenApiContact
        {
            Name = "Contact",
            Url = new Uri("http://example.com")
        },
        License = new OpenApiLicense
        {
            Name = "License",
            Url = new Uri("http://example.com")
        }
    });
    options.AddSecurityDefinition("token", new OpenApiSecurityScheme()
    {
        Name = HeaderNames.Authorization,
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "token"
                }
            },
            Array.Empty<string>()
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddTransient<ConsoleMiddleware>();

var configValue = builder.Configuration.GetValue<string>("Security:Secret");
var key = Encoding.ASCII.GetBytes(configValue);
builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(t =>
{
    t.RequireHttpsMetadata = true;
    t.SaveToken = true;
    t.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

//builder.Logging.ClearProviders();
//builder.Host.UseNLog();

builder.Services.Configure<BookstoreConfiguration>(builder.Configuration
    .GetSection(BookstoreConfiguration.BookstoreConfig));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IAuthorsRepository, AuthorsRepository>();
builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<IAuthorService, AuthorService>();

builder.Services.AddScoped<ISerialNumber, SimpleSerialNumber>();
builder.Services.AddScoped<ISerialNumber, SerialNumber>();

builder.Services.RegisterDelegateDependency();
builder.Services.AddTransient<IDependencyFactory, DependencyFactory>();

//builder.Services.AddKeyedScoped<ISerialNumber, SerialNumber>("Simple");

builder.Services.AddTransient<IComplexSerialNumber, ComplexSerialNumber>();

var app = builder.Build();

app.UseMiddleware<ConsoleMiddleware>();
app.UseMiddleware<RequestCultureMiddleware>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
