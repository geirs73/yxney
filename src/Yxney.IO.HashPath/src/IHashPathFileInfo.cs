using System.Collections.Generic;
using System.IO;

namespace Yxney.IO.HashPath;

public interface IHashPathFileInfo
{
    DirectoryLengths DirectoryLengths { get; init; }
    HashPathAlgorithm HashingMethod { get; init; }

    string GetHashedPath(string path);
    FileInfo GetHashedPathFileInfo(string path);
}