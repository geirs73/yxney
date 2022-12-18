namespace Foo.Tests;

public class HashPathTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    [TestCase("""0301-400-300-13214-0007.xml""", """03d\35620\4004b3c42294b48f8f1901a54559d502.xml""")]
    public void TestNormalScenario(string input, string expected)
    {
        Assert.That(HashPath.GetHashStructurePath(input, HashPath.HashType.SHA1, 3, 5), Is.EqualTo(expected));
    }

    [Test]
    public void TestTooManyDirectoryBytesScenario()
    {
        Assert.Throws(
            Is.TypeOf<ArgumentException>()
                .And.Message.EqualTo("Too many bytes are used for directory structure, "
                    + "try reducing it below 35 (Parameter 'bytesPerDirectoryLevel')"),
            () => HashPath.GetHashStructurePath("""0301-400-300-13214-0007.xml""", HashPath.HashType.SHA1, 3, 64));
    }
}