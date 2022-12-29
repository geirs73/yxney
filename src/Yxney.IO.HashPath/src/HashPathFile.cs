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

    public static string GetHashPath(
        string path,
        HashAlgorithmType hashMethod,
        DirectoryStructure structure)
    {
        ArgumentNullException.ThrowIfNull(structure);
        string fileExtension = Path.GetExtension(path);
        string fileName = Path.GetFileNameWithoutExtension(path);
        string? directory = Path.GetDirectoryName(path);

        byte[] clearTextFileNameData = Encoding.UTF8.GetBytes(fileName);
        byte[] hashedFileNameData = HashWithMethod(clearTextFileNameData, hashMethod);

#pragma warning disable CA1308
        string hashedFilenameDataHexString = Convert.ToHexString(hashedFileNameData).ToLowerInvariant();
#pragma warning restore CA1308

        int charactersUsedForDirectoryStructureLimit = hashedFilenameDataHexString.Length - _MinimumFileNameLength;
        if (structure.TotalLength() > charactersUsedForDirectoryStructureLimit)
        {
            //TODO: Fix wording
            throw new ArgumentException(
                "Too many characters are used for directory structure, try reducing total "
                    + $"length below {charactersUsedForDirectoryStructureLimit:0}",
                nameof(structure));
        }

        string[] parts = GetHashPathParts(directory, hashedFilenameDataHexString, fileExtension, structure);
        return Path.Combine(parts);
    }

    private static string[] GetHashPathParts(
        string? directory,
        string hashHexString,
        string extension,
        DirectoryStructure structure)
    {
        List<string> pathParts = new();

        int currentPosition = 0;

        if (directory is not null)
            pathParts.Add(directory);

        foreach (int directoryNameLength in structure)
        {
            string pathPart = hashHexString.Substring(currentPosition, directoryNameLength);
            pathParts.Add(pathPart);
            currentPosition += directoryNameLength;
        }

        string hashedFileName = hashHexString[currentPosition..];
        pathParts.Add(hashedFileName + extension);
        return pathParts.ToArray();
    }

#pragma warning disable CA5350,CA5351
    private static byte[] HashWithMethod(byte[] clearTextBytes, HashAlgorithmType hashMethod = HashAlgorithmType.MD5)
    {
        return hashMethod switch
        {
            HashAlgorithmType.SHA1 => SHA1.HashData(clearTextBytes),
            HashAlgorithmType.SHA256 => SHA256.HashData(clearTextBytes),
            HashAlgorithmType.SHA512 => SHA512.HashData(clearTextBytes),
            HashAlgorithmType.XxHash64 => XxHash64.Hash(clearTextBytes),
            HashAlgorithmType.Crc64 => Crc64.Hash(clearTextBytes),
            _ => MD5.HashData(clearTextBytes)
        };
#pragma warning restore CA5350,CA5351
    }
}
