using System.CommandLine.Binding;
using System.CommandLine.Parsing;
using System.CommandLine;

namespace CommandStructureBuilder.Commands;

public class CopyFileCommand
{
    public required FileInfo? Input { get; set; }
    public required FileInfo? Output { get; set; }

    public static async Task<int> Execute(CopyFileCommand command)
    {
        await Console.Out.WriteLineAsync("Hello world!" + command.Input?.FullName + " " + command.Output?.FullName + "verbose: ");
        return 0;
    }

    public class CopyFileCommandBinder : BinderBase<CopyFileCommand>
{
    protected Argument<FileInfo> InputArgument { get; set; }

    protected Argument<FileInfo> OutputArgument { get; set; }

    public CopyFileCommandBinder(Command command)
    {
        InputArgument = new Argument<FileInfo>("input", "description");
        OutputArgument = new Argument<FileInfo>("output", "description");
        AddArguments(command);
        SetHandler(command);
    }

    protected override CopyFileCommand GetBoundValue(BindingContext bindingContext)
    {
        ParseResult p = bindingContext.ParseResult;
        return new()
        {
            Input = p.GetValueForArgument(InputArgument),
            Output = p.GetValueForArgument(OutputArgument)
        };
    }

    public virtual void AddArguments(Command command)
    {
        command.Add(InputArgument);
        command.Add(OutputArgument);
    }

    public void SetHandler(Command command)
    {
        command.SetHandler(command => CopyFileCommand.Execute(command), this);
    }
}
}
