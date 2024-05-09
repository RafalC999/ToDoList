using FluentAssertions;

namespace TodoList.DAL.Tests
{
    public class UnitTest1
    {
        private int AddLol(int a, int b)
        {
            return a + b;
        }

        [Fact]
        public void AddLolTest1()
        {
            var a = 1;
            var b = 2;
            var result = AddLol(a, b);

            result.Should().Be(3);
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(1, 3, 4)]
        [InlineData(3, 3, 6)]
        public void AddLolTest2(int a, int b, int c)
        {
            var result = AddLol(a, b);
            result.Should().Be(c);
        }

    }
}