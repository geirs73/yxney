namespace Yxney.IO;
using System.IO;
using System.Security.Cryptography;
using System.IO.Hashing;
using System.Text;

public sealed class HashPath
{
    public IEnumerable<int> BytesPerDirectoryLevel { get; init; } = new int[] { 3 };
    public HashAlgorithms HashWith { get; init; } = HashAlgorithms.MD5;

    public enum HashAlgorithms {
        MD5,
        SHA1,
        SHA256,
        SHA512,
        XxHash32,
        XxHash64,
        Crc32,
        Crc64

    }

    public string GetPath(string path)
    {
        string extension = Path.GetExtension(path);
        string fileName = Path.GetFileNameWithoutExtension(path);
        string? directory = Path.GetDirectoryName(path);
        byte[] clearTextData = Encoding.UTF8.GetBytes(fileName);

        byte[] hashedData = HashWithSelectedAlgorithm(clearTextData);

        string hashedDataHexString = Convert.ToHexString(hashedData).ToLowerInvariant();
        string[] parts = GetHashedFilePathParts(extension, directory, hashedDataHexString);
        return Path.Combine(parts);
    }

    private string[] GetHashedFilePathParts(string extension, string? directory, string hashHexString)
    {
        List<string> parts = new();

        int currentPosition = 0;

        if (directory is not null)
            parts.Add(directory);

        foreach (int dirLevelBytes in BytesPerDirectoryLevel)
        {
            string part = hashHexString.Substring(currentPosition, dirLevelBytes);
            parts.Add(part);
            currentPosition += dirLevelBytes;
        }

        string hashedFileName = hashHexString[currentPosition..];
        parts.Add(hashedFileName + extension);
        return parts.ToArray();
    }

    private byte[] HashWithSelectedAlgorithm(byte[] clearTextBytes)
    {
        return HashWith switch
        {
            HashAlgorithms.SHA1 => SHA1.HashData(clearTextBytes),
            HashAlgorithms.SHA256 => SHA256.HashData(clearTextBytes),
            HashAlgorithms.SHA512 => SHA512.HashData(clearTextBytes),
            HashAlgorithms.XxHash32 => XxHash32.Hash(clearTextBytes),
            HashAlgorithms.XxHash64 => XxHash64.Hash(clearTextBytes),
            HashAlgorithms.Crc32 => Crc32.Hash(clearTextBytes),
            HashAlgorithms.Crc64 => Crc64.Hash(clearTextBytes),
            _ => MD5.HashData(clearTextBytes)
        };
    }
}
