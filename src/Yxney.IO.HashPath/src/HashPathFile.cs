using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Hashing;
using System.Security.Cryptography;
using System.Text;

namespace Yxney.IO.HashPath;

public static class HashPathFile
{
    private const int _MinimumFileNameLength = 5;

    public static string GetHashedPath(
        string path,
        HashPathAlgorithm hashAlgorithm,
        DirectoryLengths directoryLengths)
    {
        ArgumentNullException.ThrowIfNull(directoryLengths);
        string extension = Path.GetExtension(path);
        string fileName = Path.GetFileNameWithoutExtension(path);
        string? directory = Path.GetDirectoryName(path);

        byte[] clearTextData = Encoding.UTF8.GetBytes(fileName);
        byte[] hashedData = HashWithSelectedAlgorithm(clearTextData, hashAlgorithm);

#pragma warning disable CA1308
        string hashedDataHexString = Convert.ToHexString(hashedData).ToLowerInvariant();
#pragma warning restore CA1308

        int limit = hashedDataHexString.Length - _MinimumFileNameLength;
        if (directoryLengths.TotalLength() > limit)
        {
            throw new ArgumentException(
                $"Too many bytes are used for directory structure, try reducing it below {limit:0}",
                nameof(directoryLengths));
        }

        string[] parts = GetHashedFilePathParts(extension, directory, hashedDataHexString, directoryLengths);
        return Path.Combine(parts);
    }

    private static string[] GetHashedFilePathParts(
        string extension,
        string? directory,
        string hashHexString,
        DirectoryLengths bytesPerDirectoryLevel)
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

#pragma warning disable CA5350,CA5351
    private static byte[] HashWithSelectedAlgorithm(byte[] clearTextBytes, HashPathAlgorithm hashWith = HashPathAlgorithm.MD5)
    {
        return hashWith switch
        {
            HashPathAlgorithm.SHA1 => SHA1.HashData(clearTextBytes),
            HashPathAlgorithm.SHA256 => SHA256.HashData(clearTextBytes),
            HashPathAlgorithm.SHA512 => SHA512.HashData(clearTextBytes),
            HashPathAlgorithm.XxHash64 => XxHash64.Hash(clearTextBytes),
            HashPathAlgorithm.Crc64 => Crc64.Hash(clearTextBytes),
            _ => MD5.HashData(clearTextBytes)
        };
#pragma warning restore CA5350,CA5351
    }
}
