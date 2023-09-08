using FluentAssertions;

namespace Book.Test
{
    public class BookRepository_Tests
    {
        public static IEnumerable<object[]> BookList =>
            new List<object[]>
            {
                new object[]
                {
                    new List<Book> 
                    {  
                        new Book { Id = 1, Name = "Name 1"},
                        new Book { Id = 2, Name = "Name 2"},
                        new Book { Id = 3, Name = "Name 3"}
                    },
                    5,
                }
            };

        [Fact]
        private void GetBookName_LessThan10_ReturnBookOne()
        {
            // Arrange
            var sut = new BookRepository();

            // Act
            var result = sut.GetBookName(9);

            //Assert

            Assert.Equal("Book one", result);
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(1, 5, 6)]
        [InlineData(7, 2, 9)]
        public void AddMethod_AddNumbers_ShoudBeCorrect(int a, int b, int expected)
        {
            // Arrange
            var sut = new BookRepository();

            //Act

            var result = sut.AddMethod(a, b);

            //Assert
            result.Should().Be(expected);     
        }
        [Theory, MemberData(nameof(BookList))]
        public void CountBooks_GetAll_ShouldBeCorrect(IEnumerable<Book> books, int expected)
        {
            var sut = new BookRepository();

            var result = sut.CountBooks(books);

            result.Should().Be(expected);
        }
    }
}