using System.CommandLine.Binding;
using System.CommandLine.Parsing;
using System.CommandLine;
using CommandStructureBuilder.Commands;
using Yxney.CommandLine.Handler;
using Yxney.CommandLine.Extension;

namespace CommandStructureBuilder.Binders;

public class CopyFileCommandHandler : BinderBase<CopyFileCommand>, ICommandHandler
{
    protected Argument<FileInfo> InputArgument { get; set; }
    protected Argument<FileInfo> OutputArgument { get; set; }
    protected Option<bool> ForceOption { get; set; }
    public Command Command { get; }

    public CopyFileCommandHandler()
    {
        InputArgument = new Argument<FileInfo>("input", "description");
        OutputArgument = new Argument<FileInfo>("output", "description");
        ForceOption = new Option<bool>("--force", "Force copy operation").AddAlias<bool>("-f");
        Command = new("copy-file", "Copy file.")
        {
            InputArgument,
            OutputArgument,
            ForceOption
        };
        Command.SetHandler(command => CopyFileCommand.ExecuteAsync(command), this);
    }

    protected override CopyFileCommand GetBoundValue(BindingContext bindingContext)
    {
        ParseResult p = bindingContext.ParseResult;
        return new()
        {
            Input = p.GetValueForArgument(InputArgument),
            Output = p.GetValueForArgument(OutputArgument),
            Force = p.GetValueForOption(ForceOption)
        };
    }
}
