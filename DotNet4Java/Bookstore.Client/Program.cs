using Bookstore.Client;
using Bookstore.Client.Data;
using Bookstore.Client.Hubs;
using Bookstore.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddMudServices(options => options.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter);
builder.Services.AddDbContextFactory<BookstoreContext>(options => options.UseSqlServer("Server=DESKTOP-RQ4JEVV;Database=Bookstore;Integrated Security=true; TrustServerCertificate=true;"));
builder.Services.AddResponseCompression( o =>
{
    o.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
});
builder.Services.AddScoped<StateContainer>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseResponseCompression();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();

app.MapHub<ChatHub>("/chathub");

app.MapFallbackToPage("/_Host");

app.Run();
