using System.IO;

namespace Yxney.IO.HashPath;

public static class FileInfoExtensions
{
    public static FileInfo GetHashPathFileInfo(this FileInfo fileInfo, HashPathAlgorithm hashMethod, DirectoryLengths structure)
    {
        HashPathFileInfo hashPathFileInfo = new() { DirectoryLengths = structure, HashingMethod = hashMethod };
        return hashPathFileInfo.GetHashedPathFileInfo(fileInfo);
    }
}