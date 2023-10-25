using Microsoft.AspNetCore.Components;

namespace Bookstore.Client.Shared
{
    public partial class TestComponent<TItem> : ComponentBase
    {
        private int testNumber = 1;

        private string testParagraph = "first";
        
        [Parameter]
        public List<TItem> Items { get; set; } = new List<TItem>();

        [Parameter]
        public EventCallback<int> ClickCallback { get; set; }

        //protected override async Task OnParametersSetAsync()
        //{
            
        //}

        //protected override Task OnInitializedAsync()
        //{
        //    return base.OnInitializedAsync();
        //}

        //protected override Task OnAfterRenderAsync(bool firstRender)
        //{
        //    return base.OnAfterRenderAsync(firstRender);
        //}

        private async Task UpdateHandlerAsync(string input)
        {
            testParagraph = input;
            await InvokeAsync(StateHasChanged);
            testParagraph = "";
            await InvokeAsync(StateHasChanged);
            await ClickCallback.InvokeAsync(1);
        }
    }
}
