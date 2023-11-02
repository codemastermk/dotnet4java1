using Microsoft.AspNetCore.Components;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Bookstore.Data;
using Bookstore.Data.Models;
using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace Bookstore.Client.Pages
{
    public partial class WebApiComponent : ComponentBase
    {
        private List<Author> Authors { get; set; } = new ();

        [Inject]
        private IHttpClientFactory HttpClientFactory { get; set; }

        [Inject] 
        private IDbContextFactory<BookstoreContext> dbContextFactory { get; set; }

        [Inject]
        private IJSRuntime jSRuntime { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            //using var context = await dbContextFactory.CreateDbContextAsync();
            //Authors = await context.Authors.ToListAsync();
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44347/api/Authors/GetAuthors");
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("User-Agent", "Simple");

                var client = HttpClientFactory.CreateClient();

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Authors = await JsonSerializer.DeserializeAsync<List<Author>>(responseStream, options);
                }
            }
            catch (Exception e)
            { 
                Console.WriteLine(e.Message);
            }
            
        }

        private async Task Alert()
        {
            //await jSRuntime.InvokeVoidAsync("displayTickerAlert1");
            var result = await jSRuntime.InvokeAsync<string>("displayTickerAlert1");
        }

        [JSInvokable]
        public static Task<int[]> ReturnArrayAsync()
        {
            return Task.FromResult(new int[] {1, 2, 3});
        }
    }
}
