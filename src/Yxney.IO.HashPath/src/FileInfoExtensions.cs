using System;
using System.IO;

namespace Yxney.IO.HashPath;

public static class FileInfoExtensions
{
    public static FileInfo GetHashPathFileInfo(this FileInfo fileInfo, HashAlgorithmType hashMethod, DirectoryStructure structure)
    {
        ArgumentNullException.ThrowIfNull(fileInfo);
        string hashPath = HashPathFile.GetHashPath(fileInfo.FullName, hashMethod, structure);
        return new FileInfo(hashPath);
    }
}