namespace Foo.Tests;

#pragma warning disable RCS0056

[ExcludeFromCodeCoverage]
public class HashPathTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    [TestCase(
        """0301-400-300-13214-0007.xml""",
        """ba9\61769\465c20d911d7f63874ddf2e8.xml""",
        HashAlgorithmType.MD5)]
    [TestCase(
        """0301-400-300-13214-0007.xml""",
        """03d\35620\4004b3c42294b48f8f1901a54559d502.xml""",
        HashAlgorithmType.SHA1)]
    [TestCase(
        """0301-400-300-13214-0007.xml""",
        """412\bb27b\8b33a504.xml""",
        HashAlgorithmType.Crc64)]
    [TestCase(
        """0301-400-300-13214-0007.xml""",
        """039\64d61\eb8c376ffab7a7132d23bd3742edc9362bbb30731a0ca1c3509515b0.xml""",
        HashAlgorithmType.SHA256)]
    [TestCase(
        """0301-400-300-13214-0007.xml""",
        """0e5\3ecaa\3d15fd5caa84542cb043b4b6c0aab3ebe202e34537719b900658f8ac93cd2560fa2dbe76a1383153c05625c37d7bf9e44f80388dba12daec5b695fe5.xml""",
        HashAlgorithmType.SHA512)]
    [TestCase(
        """0301-400-300-13214-0007.xml""",
        """b5a\0f9b4\c9bb837e.xml""",
        HashAlgorithmType.XxHash64)]
    public void TestNormalScenario(string input, string expected, HashAlgorithmType hashType)
    {
        Assert.That(HashPathFile.GetHashPath(input, hashType, DirectoryStructure.Create(3, 5)), Is.EqualTo(expected));
    }

    [Test]
    [TestCase(
        """0301-400-300-13214-0007.xml""",
        """ba9\61769\465c20d911d7f63874ddf2e8.xml""",
        HashAlgorithmType.MD5)]
    [TestCase(
        """0301-400-300-13214-0007.xml""",
        """03d\35620\4004b3c42294b48f8f1901a54559d502.xml""",
        HashAlgorithmType.SHA1)]
    [TestCase(
        """0301-400-300-13214-0007.xml""",
        """412\bb27b\8b33a504.xml""",
        HashAlgorithmType.Crc64)]
    [TestCase(
        """0301-400-300-13214-0007.xml""",
        """039\64d61\eb8c376ffab7a7132d23bd3742edc9362bbb30731a0ca1c3509515b0.xml""",
        HashAlgorithmType.SHA256)]
    [TestCase(
        """0301-400-300-13214-0007.xml""",
        """0e5\3ecaa\3d15fd5caa84542cb043b4b6c0aab3ebe202e34537719b900658f8ac93cd2560fa2dbe76a1383153c05625c37d7bf9e44f80388dba12daec5b695fe5.xml""",
        HashAlgorithmType.SHA512)]
    [TestCase(
        """0301-400-300-13214-0007.xml""",
        """b5a\0f9b4\c9bb837e.xml""",
        HashAlgorithmType.XxHash64)]
    public void TestNormalScenarioObjectVersion(string input, string expected, HashAlgorithmType hashType)
    {
        HashPathFileInfo hp = new()
        {
            DirectoryStructure = DirectoryStructure.Create(3, 5),
            HashMethod = hashType
        };

        string path = hp.GetPath(input);
        Assert.That(path, Is.EqualTo(expected));
        FileInfo fileInfo = hp.GetFileInfo(input);
        string fileInfoRelativeDir = Path.GetRelativePath(Environment.CurrentDirectory, fileInfo.DirectoryName!);
        string fileInfoPathRelative = Path.Combine(fileInfoRelativeDir, fileInfo.Name);
        Assert.That(fileInfoPathRelative, Is.EqualTo(expected));
    }

    [Test]
    public void TestTooManyDirectoryBytesScenario()
    {
        Assert.Throws(
            Is.TypeOf<ArgumentException>()
                .And.Message.EqualTo("Too many characters are used for directory structure, try reducing total length below 35 (Parameter 'structure')"),
            () => HashPathFile.GetHashPath("""0301-400-300-13214-0007.xml""", HashAlgorithmType.SHA1, DirectoryStructure.Create(3, 64)));
    }

    [Test]
    public void TestDefaultPath()
    {
        Assert.That(
            HashPathFile.GetHashPath("""0301-400-300-13214-0007.xml""", HashAlgorithmType.MD5, DirectoryStructure.Create()),
            Is.EqualTo("""ba9\61769465c20d911d7f63874ddf2e8.xml"""));
    }

    [Test]
    public void TestFileInfoExtensions()
    {
        FileInfo normalFileInfo = new("""0301-400-300-13214-0007.xml""");
        FileInfo hashedFileInfo = normalFileInfo.GetHashPathFileInfo(HashAlgorithmType.MD5, DirectoryStructure.Create(3, 5));

        string fileInfoRelativeDir = Path.GetRelativePath(Environment.CurrentDirectory, hashedFileInfo.DirectoryName!);
        string fileInfoPathRelative = Path.Combine(fileInfoRelativeDir, hashedFileInfo.Name);
        Assert.That(fileInfoPathRelative, Is.EqualTo("""ba9\61769\465c20d911d7f63874ddf2e8.xml"""));
    }
}
#pragma warning restore RCS0056
