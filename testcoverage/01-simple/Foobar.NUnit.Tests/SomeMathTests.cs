namespace Foobar.NUnit.Tests;

public class SomeMathTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void AddTest()
    {
        Assert.That(SomeMath.Add(5, 4), Is.EqualTo(9));
    }

    [Test]
    public void EvenNumbersInArrayTest()
    {
        Assert.That(SomeMath.EvenNumbersInArray(new int[] { 3, 3, 5, 1, 9 }), Is.EqualTo(new int[] { }));
    }

}