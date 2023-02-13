namespace CommandStructureBuilder.Commands;

public partial class CopyFileCommand
{
    public required FileInfo? Input { get; set; }
    public required FileInfo? Output { get; set; }

    public static async Task<int> Execute(CopyFileCommand command)
    {
        await Console.Out.WriteLineAsync("Hello world!" + command.Input?.FullName + " " + command.Output?.FullName + "verbose: ");
        return 0;
    }
}
