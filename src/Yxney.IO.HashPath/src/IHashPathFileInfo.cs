using System.IO;

namespace Yxney.IO.HashPath;

public interface IHashPathFileInfo
{
    DirectoryLengths DirectoryLengths { get; init; }
    HashPathAlgorithm HashingMethod { get; init; }

    string GetHashedPath(string filePath);
    FileInfo GetHashedPathFileInfo(FileInfo fileInfo);
    FileInfo GetHashedPathFileInfo(string filePath);
}