using System.CommandLine.Binding;
using System.CommandLine.Parsing;
using System.CommandLine;

namespace CommandStructureBuilder.Commands;

public class CopyFileCommandBinder : BinderBase<CopyFileCommand>, ICommandBinder
{
    protected Argument<FileInfo> InputArgument { get; set; }

    protected Argument<FileInfo> OutputArgument { get; set; }

    public CopyFileCommandBinder()
    {
        InputArgument = new Argument<FileInfo>("input", "description");
        OutputArgument = new Argument<FileInfo>("output", "description");
    }

    public void Register(Command command)
    {
        command.Add(InputArgument);
        command.Add(OutputArgument);
        command.SetHandler(command => CopyFileCommand.Execute(command), this);
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
}
