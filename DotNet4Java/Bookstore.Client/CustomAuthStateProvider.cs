using Bookstore.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;

namespace Bookstore.Client
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly string _authType = "Custom Authentication";

        private readonly IDbContextFactory<BookstoreContext> _dbContextFactory;

        private readonly ProtectedLocalStorage _protectedLocalStorage;

        public CustomAuthStateProvider(IDbContextFactory<BookstoreContext> dbContextFactory, ProtectedLocalStorage protectedLocalStorage)
        {
            _dbContextFactory = dbContextFactory;
            _protectedLocalStorage = protectedLocalStorage;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = new ClaimsPrincipal();
            return Task.FromResult(new AuthenticationState(user));
        }

        //public async Task AuthenticateUserAsync()
        //{
        //    var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "Petar"), new Claim(ClaimTypes.Role, "Admin") }, _authType);
        //    var user = new ClaimsPrincipal(identity);
        //    NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        //}

        public async Task AuthenticateUserAsync(string userName, string password)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var user = new ClaimsPrincipal();
            if(!string.IsNullOrEmpty(userName) && userName == "Petar" && !string.IsNullOrEmpty(password) && password == "Petar")
            {
                var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "Petar"), new Claim(ClaimTypes.Role, "SuperAdmin"), new Claim(ClaimTypes.Role, "Admin"), new Claim(ClaimTypes.Role, "User") }, _authType);
                user = new ClaimsPrincipal(identity);       
                await _protectedLocalStorage.SetAsync("user", "Petar, Petar");
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async Task Logout()
        {
            await _protectedLocalStorage.DeleteAsync("user");
            var user = new ClaimsPrincipal();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}
