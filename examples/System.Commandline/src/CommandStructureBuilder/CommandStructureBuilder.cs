namespace CommandStructureBuilder
{
    public interface ICommandStructureElement
    {
    }

    public class CommandStructureBuilder
    {
        private readonly Dictionary<string, ICommandStructureElement> _dictionary = new();

        public CommandStructureBuilder Add(ICommandStructureElement parent, ICommandStructureElement child)
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

        public CommandStructureBuilder Add(ICommandStructureElement root)
        {
            return this;
        }
    }
}