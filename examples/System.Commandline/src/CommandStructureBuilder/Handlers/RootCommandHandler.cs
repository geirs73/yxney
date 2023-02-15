using System.CommandLine;
using Yxney.CommandLine.Handler;

namespace CommandStructureBuilder.Binders;

public class RootCommandHandler : ICommandHandler
{
    public Command Command => RootCommand;
    public RootCommand RootCommand { get; }

    public RootCommandHandler()
    {
        RootCommand = new RootCommand("Tool for running OpenPolicyAgent on windows developer computers.");
    }
}