using System.CommandLine;

namespace Yxney.CommandLine.Extension;

public static class CommandLineExtensions
{
    public static Option<T> AddAlias<T>(this Option<T> option, string alias)
    {
        option.AddAlias(alias);
        return option;
    }
}