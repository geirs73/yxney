using System;
using System.IO;

namespace Yxney.IO.HashPath;

public class HashPathFileInfo : IHashPathFileInfo
{
    public DirectoryStructure DirectoryStructure { get; init; } = DirectoryStructure.Create();
    public HashAlgorithmType HashMethod { get; init; } = HashAlgorithmType.XxHash64;

    public string GetPath(string filePath)
    {
        return HashPathFile.GetHashPath(filePath, HashMethod, DirectoryStructure);
    }

    public FileInfo GetFileInfo(string filePath)
    {
        ArgumentNullException.ThrowIfNull(filePath);
        string hashedPath = GetPath(filePath);
        return new FileInfo(hashedPath);
    }
}
