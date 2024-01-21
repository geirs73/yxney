namespace DllTreeCmd;

internal class Program
{
    // this program uses DragonFruit magic, because convenience

    public enum OutputFormat
    {
        Csv,
        Txt,
        Json
    }

    /// <param name="path">Path to folder</param>
    /// <param name="pattern">File pattern to match</param>
    /// <param name="exclude">Semi-colon separated list of strings to exclude</param>
    /// <param name="format">Output format, only CSV for now</param>
    public static void Main(
        string path = ".",
        string pattern = "*.*",
        string exclude = "obj",
        OutputFormat format = OutputFormat.Csv)
    {
        switch (format)
        {
            case OutputFormat.Csv:
                break;

            default:
                throw new Exception("Only CSV format is supported at this time");
        }

        DirectoryInfo dir = new(path);

        FileInfo[] files = dir.GetFiles(pattern, SearchOption.AllDirectories);

        HashSet<string> exclusions = exclude
            .Split(';')
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        IEnumerable<FileInfo> nonExcludedFiles = files
            .Where(f => !exclusions.Contains(f.FullName, StringComparer.OrdinalIgnoreCase));

        HashSet<string> assemblyExtensions = [".exe", "dll"];
        List<FileInfo> exeAndDllFiles = nonExcludedFiles
            .Where(f => assemblyExtensions.Contains(f.Extension.ToLowerInvariant()))
            .ToList();

        IEnumerable<string> filesStringArray = exeAndDllFiles.Select(f => f.FullName);

        PathAssemblyResolver resolver = new(filesStringArray);

        Console.Out.WriteLine("sep=;");
        Console.Out.WriteLine($"Path;ProductVersion;FileVersion;LastWrittenTime");

        using MetadataLoadContext metaDataContext = new(resolver);

        foreach (FileInfo f in exeAndDllFiles)
        {
            Assembly assembly = metaDataContext.LoadFromAssemblyPath(f.FullName);
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            string versionString = version.ToString(4);
            FileVersionInfo fileVersion = FileVersionInfo.GetVersionInfo(f.FullName);

            string formattedTime = f.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
            Console.Out.WriteLine($"{f.DirectoryName};{f.Name};{versionString};{fileVersion.ProductVersion};{fileVersion.FileVersion};{formattedTime}");
        }
    }
}
