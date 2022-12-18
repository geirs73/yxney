using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Hashing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Yxney.IO.HashDistributedPath;

public sealed class HashPath
{
    public IEnumerable<int> BytesPerDirectoryLevel { get; init; } = new int[] { 3 };
    public HashType HashingMethod { get; init; } = HashType.MD5;

    private const int _MinimumFileNameLength = 5;

    public enum HashType
    {
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
        return HashPath.GetHashStructurePath(path, HashingMethod, BytesPerDirectoryLevel.ToArray());
    }

    public static string GetHashStructurePath(
        string path,
        HashType hashAlgorithm = HashType.XxHash64,
        params int[]? bytesPerDirectoryLevel)
    {
        bytesPerDirectoryLevel ??= new int[] { 3 };
var foo = new FileInfo("Barrier.zot");

        string extension = Path.GetExtension(path);
        string fileName = Path.GetFileNameWithoutExtension(path);
        string? directory = Path.GetDirectoryName(path);

        byte[] clearTextData = Encoding.UTF8.GetBytes(fileName);
        byte[] hashedData = HashWithSelectedAlgorithm(clearTextData, hashAlgorithm);

#pragma warning disable CA1308
        string hashedDataHexString = Convert.ToHexString(hashedData).ToLowerInvariant();
#pragma warning restore CA1308

        int dirLength = bytesPerDirectoryLevel.Sum(x => x);
        int limit = (hashedDataHexString.Length - _MinimumFileNameLength);
        if (dirLength > limit)
        {
            throw new ArgumentException(
                $"Too many bytes are used for directory structure, try reducing it below {limit}",
                nameof(bytesPerDirectoryLevel));
        }

        string[] parts = GetHashedFilePathParts(extension, directory, hashedDataHexString, bytesPerDirectoryLevel);
        return Path.Combine(parts);
    }

    private static string[] GetHashedFilePathParts(
        string extension,
        string? directory,
        string hashHexString,
        IEnumerable<int> bytesPerDirectoryLevel)
    {
        List<string> parts = new();

        int currentPosition = 0;

        if (directory is not null)
            parts.Add(directory);

        foreach (int dirLevelBytes in bytesPerDirectoryLevel)
        {
            string part = hashHexString.Substring(currentPosition, dirLevelBytes);
            parts.Add(part);
            currentPosition += dirLevelBytes;
        }

        string hashedFileName = hashHexString[currentPosition..];
        parts.Add(hashedFileName + extension);
        return parts.ToArray();
    }

    private static byte[] HashWithSelectedAlgorithm(byte[] clearTextBytes, HashType hashWith = HashType.MD5)
    {
        return hashWith switch
        {
            HashType.SHA1 => SHA1.HashData(clearTextBytes),
            HashType.SHA256 => SHA256.HashData(clearTextBytes),
            HashType.SHA512 => SHA512.HashData(clearTextBytes),
            HashType.XxHash32 => XxHash32.Hash(clearTextBytes),
            HashType.XxHash64 => XxHash64.Hash(clearTextBytes),
            HashType.Crc32 => Crc32.Hash(clearTextBytes),
            HashType.Crc64 => Crc64.Hash(clearTextBytes),
            _ => MD5.HashData(clearTextBytes)
        };
    }
}
