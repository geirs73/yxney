using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Yxney.IO
{
    public class HashPathFileInfo : IHashPathFileInfo
    {
        public IEnumerable<int> BytesPerDirectoryLevel { get; init; } = new int[] { 3 };
        public HashType HashingMethod { get; init; } = HashType.MD5;

        public string GetHashedPath(string path)
        {
            return HashPathFile.GetHashedPath(path, HashingMethod, BytesPerDirectoryLevel.ToArray());
        }

        public FileInfo GetHashedPathFileInfo(string path)
        {
            var filePath = GetHashedPath(path);
            return new FileInfo(filePath);
        }
    }
}