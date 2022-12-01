namespace Foobar.XUnit.Tests;

public class SomeMoreMathTests
{
    [Fact]
    public void DivideTest()
    {
        Assert.Equal<int>(2, SomeMath.Divide(4, 2));
    }

    [Fact]
    public void DivideFailTest()
    {
        Assert.Throws<DivideByZeroException>(() => SomeMath.Divide(5, 0));
    }

    [Fact]
    public void TestMaxValueInArray()
    {
        Assert.Equal<int>(300, SomeMath.MaxValueInArray(new []{3,5,9,11,300,14,31}));
    }
}