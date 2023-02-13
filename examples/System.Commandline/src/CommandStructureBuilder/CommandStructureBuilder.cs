using System.CommandLine;

namespace CommandStructureBuilder;

public interface ICommandStructureBuilder
{
    CommandStructureBuilder Add(ICommandBinder parent, ICommandBinder child);
    CommandStructureBuilder Add(ICommandBinder root);
}

public class CommandStructureBuilder : ICommandStructureBuilder
{
    private readonly Dictionary<string, ICommandBinder> _dictionary = new();

    public CommandStructureBuilder Add(ICommandBinder parent, ICommandBinder child)
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

    public CommandStructureBuilder Add(ICommandBinder root)
    {
        string key = root.GetType().Name;
        if (_dictionary.Count > 0) throw new ArgumentException("You need to add the root first");
        _dictionary.Add(key, root);
        return this;
    }
}