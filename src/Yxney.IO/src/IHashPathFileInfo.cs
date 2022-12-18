using System.Collections.Generic;
using System.IO;

namespace Yxney.IO
{
    public interface IHashPathFileInfo
    {
        IEnumerable<int> BytesPerDirectoryLevel { get; init; }
        HashType HashingMethod { get; init; }

        string GetHashedPath(string path);
        FileInfo GetHashedPathFileInfo(string path);
    }
}