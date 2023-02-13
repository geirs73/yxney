namespace CommandStructureBuilder.Commands;

public class CopyFileCommand
{
    public required FileInfo? Input { get; set; }
    public required FileInfo? Output { get; set; }
    public bool Force { get; set; } = default!;

    public static async Task<int> Execute(CopyFileCommand command)
    {
        await Console.Out.WriteLineAsync("Hello world!" + command.Input?.FullName + " " + command.Output?.FullName + $"force: {command.Force}");
        return 0;
    }
}
