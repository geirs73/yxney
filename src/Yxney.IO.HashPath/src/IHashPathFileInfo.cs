using System.IO;

namespace Yxney.IO.HashPath;

public interface IHashPathFileInfo
{
    DirectoryStructure DirectoryStructure { get; init; }
    HashAlgorithmType HashMethod { get; init; }

    string GetPath(string filePath);
    FileInfo GetFileInfo(string filePath);
}