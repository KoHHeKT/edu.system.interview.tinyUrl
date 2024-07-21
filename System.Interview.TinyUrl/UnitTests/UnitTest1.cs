using System.Interview.TinyUrl.ByHash.BusinessLogic;
using Xunit.Abstractions;

namespace UnitTests;

public class UnitTest1
{
    private readonly ITestOutputHelper testOutputHelper;

    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Test1()
    {
        //todo: write tests for all int range
        for (byte i = 0; i < byte.MaxValue; i++)
        {
            var encodeInt = Base62Encoder.Encode(i);
            var encodeByte = Base62Encoder.Encode(new[] { i });

            testOutputHelper.WriteLine($"{i}:");
            testOutputHelper.WriteLine($"{encodeInt}");
            testOutputHelper.WriteLine($"{encodeByte}");
            testOutputHelper.WriteLine("=========================");
            Assert.Equal(encodeInt, encodeByte);
        }
    }
}
