using System.CommandLine;

namespace CommandStructureBuilder;

public interface ICommandBinder
{
    void Register(Command command);
}