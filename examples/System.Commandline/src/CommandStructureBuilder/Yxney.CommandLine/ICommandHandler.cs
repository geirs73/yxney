using System.CommandLine;

namespace Yxney.CommandLine.Handler;

public interface ICommandHandler
{
    Command Command { get; }
}