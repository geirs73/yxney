using System.IO;

namespace Yxney.IO.HashPath;

public class HashPathFileInfo : IHashPathFileInfo
{
    public DirectoryLengths DirectoryLengths { get; init; } = DirectoryLengths.Create();
    public HashPathAlgorithm HashingMethod { get; init; } = HashPathAlgorithm.XxHash64;

    public string GetHashedPath(string path)
    {
        return HashPathFile.GetHashedPath(path, HashingMethod, DirectoryLengths);
    }

    public FileInfo GetHashedPathFileInfo(string path)
    {
        var filePath = GetHashedPath(path);
        return new FileInfo(filePath);
    }
}
