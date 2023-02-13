using System.CommandLine;
using Yxney.CommandLine.Handler;

namespace Yxney.CommandLine.Structure;

public interface ICommandStructureBuilder
{
    CommandStructureBuilder Add(ICommandHandler parent, ICommandHandler child);
    CommandStructureBuilder Add(ICommandHandler root);
}

public class CommandStructureBuilder : ICommandStructureBuilder
{
    private readonly Dictionary<string, ICommandHandler> _dictionary = new();

    public CommandStructureBuilder Add(ICommandHandler parent, ICommandHandler child)
    {
        string key = child.GetType().Name;
        string parentKey = parent.GetType().Name;
        if (_dictionary.ContainsKey(parentKey))
        {
            throw new ArgumentException("Parent command not found.", nameof(parent));
        }
        if (_dictionary.ContainsKey(key))
        {
            _dictionary.Add(key, child);
        }
        return this;
    }

    public CommandStructureBuilder Add(ICommandHandler root)
    {
        string key = root.GetType().Name;
        if (_dictionary.Count > 0) throw new ArgumentException("You need to add the root first");
        _dictionary.Add(key, root);
        return this;
    }
}