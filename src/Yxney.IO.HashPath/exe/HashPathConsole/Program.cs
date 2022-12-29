// See https://aka.ms/new-console-template for more information

using System.Text;

#pragma warning disable CA5350

string foo = Path.Combine("foo", "bar");
Console.WriteLine(foo);

for (int i = 0; i < 10; i++)
{
    string manyFileName = $"0301-400-300-13214-{i:0000}";
    byte[] textBytes = Encoding.UTF8.GetBytes(manyFileName);
    byte[] hashBytes1 = SHA1.HashData(textBytes);
    byte[] hashBytes2 = MD5.HashData(textBytes);
    byte[] hashBytes3 = Crc64.Hash(textBytes);
    byte[] hashBytes4 = XxHash64.Hash(textBytes);

    string hashHexString1 = Convert.ToHexString(hashBytes1).ToLowerInvariant();
    string hashHexString2 = Convert.ToHexString(hashBytes2).ToLowerInvariant();
    string hashHexString3 = Convert.ToHexString(hashBytes3).ToLowerInvariant();
    string hashHexString4 = Convert.ToHexString(hashBytes4).ToLowerInvariant();

    Console.WriteLine($"{manyFileName}: {hashHexString1} {hashHexString2} {hashHexString3} {hashHexString4}");
}

var hp = new HashPathFileInfo { HashMethod = HashAlgorithmType.SHA1 };
for (int i = 0; i < 10; i++)
{
    string manyFileName = $"0301-400-300-13214-{i:0000}.xml";
    string hpString = hp.GetPath(manyFileName);
    Console.WriteLine($"{manyFileName}: {hpString}");
}

var hp2 = new HashPathFileInfo { HashMethod = HashAlgorithmType.SHA1, DirectoryStructure = DirectoryStructure.Create(5,3,7) };
for (int i = 0; i < 10; i++)
{
    string manyFileName = $"0301-400-300-13214-{i:0000}.xml";
    string hpString = hp2.GetPath(manyFileName);
    Console.WriteLine($"{manyFileName}: {hpString}");
}

for (int i = 0; i < 10; i++)
{
    string manyFileName = $"0301-400-300-13214-{i:0000}.xml";
    string hpString = HashPathFile.GetHashPath(manyFileName, HashAlgorithmType.SHA1, DirectoryStructure.Create(3,5));
    Console.WriteLine($"{manyFileName}: {hpString}");
}

for (int i = 0; i < 10; i++)
{
    string manyFileName = $"0301-400-300-13214-{i:0000}.xml";
    string hpString = HashPathFile.GetHashPath(manyFileName, HashAlgorithmType.SHA512, DirectoryStructure.Create(3,5));
    Console.WriteLine($"{manyFileName}: {hpString}");
}

#pragma warning restore CA5350
