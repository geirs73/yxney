using System;
using System.IO;

namespace Yxney.IO.HashPath;

public class HashPathFileInfo : IHashPathFileInfo
{
    public DirectoryLengths DirectoryLengths { get; init; } = DirectoryLengths.Create();
    public HashPathAlgorithm HashingMethod { get; init; } = HashPathAlgorithm.XxHash64;

    public string GetHashedPath(string filePath)
    {
        return HashPathFile.GetHashedPath(filePath, HashingMethod, DirectoryLengths);
    }

    public FileInfo GetHashedPathFileInfo(FileInfo fileInfo)
    {
        ArgumentNullException.ThrowIfNull(fileInfo);
        return GetHashedPathFileInfo(fileInfo.FullName);
    }

    public FileInfo GetHashedPathFileInfo(string filePath)
    {
        ArgumentNullException.ThrowIfNull(filePath);
        string hashedPath = GetHashedPath(filePath);
        return new FileInfo(hashedPath);
    }
}
