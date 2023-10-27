using Bookstore.Client.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Bookstore.Client.Shared
{
    public partial class TestComponent<TItem> : ComponentBase, IDisposable
    {
        private int testNumber = 1;

        private string testParagraph = "first";
        
        [Parameter]
        public List<TItem> Items { get; set; } = new List<TItem>();

        [Parameter]
        public EventCallback<int> ClickCallback { get; set; }

        [Inject]
        private ProtectedSessionStorage ProtectedSessionStorage { get; set; }

        [Inject]
        private StateContainer _stateContainer { get; set; }

        //protected override async Task OnParametersSetAsync()
        //{

        //}

        protected override void OnInitialized()
        {
            _stateContainer.OnChange += StateHasChanged;
        }

        //protected override Task OnAfterRenderAsync(bool firstRender)
        //{
        //    return base.OnAfterRenderAsync(firstRender);
        //}

        private async Task UpdateHandlerAsync(string input)
        {
            await ProtectedSessionStorage.SetAsync("rowNumber", input);
            var result = await ProtectedSessionStorage.GetAsync<string>("rowNumber");
            await ProtectedSessionStorage.DeleteAsync("rowNumber");
            testParagraph = input;
            await InvokeAsync(StateHasChanged);
            testParagraph = "";
            await InvokeAsync(StateHasChanged);
            await ClickCallback.InvokeAsync(1);
        }

        private async Task UpdatePropertyValueAsync()
        {
            _stateContainer.Property = "New Value";
        }

        public void Dispose()
        {
            _stateContainer.OnChange -= StateHasChanged;
        }
    }
}
