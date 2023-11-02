using Bunit;
using Xunit;
using Bookstore.Client.Pages;

namespace Bookstore.Client.Tests
{
    public class CounterTests
    {
        [Fact]
        public void ShoudIncremntWhenClicked()
        {
            using var ctx = new TestContext();
            var cut = ctx.RenderComponent<Counter>();
            var paraElm = cut.Find("p");
            cut.Find("button").Click();

            var paraElmText = paraElm.TextContent;
            paraElmText.MarkupMatches("Current count: 1");
        }
        
        [Theory]
        [InlineData("Current count: 1")]
        public void ShoudIncremntClicked(string result)
        {
            using var ctx = new TestContext();
            var cut = ctx.RenderComponent<Counter>();
            var paraElm = cut.Find("p");
            cut.Find("button").Click();

            var paraElmText = paraElm.TextContent;
            paraElmText.MarkupMatches(result);
        }
    }
}
