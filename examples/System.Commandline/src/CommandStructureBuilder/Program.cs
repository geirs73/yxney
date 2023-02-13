
using System.CommandLine;

namespace CommandStructureBuilder;

internal static class Program
{
    private static async Task<int> Main(string[] args)
    {
        Command coberturaCommand = new("cobertura", "Convert to cobertura.");
        // var coberturaBinder = new CoberturaCommandBinder(coberturaCommand);

        var rootCommand = new RootCommand("Tool for running OpenPolicyAgent on windows developer computers.");

        //RootCommandHelper.CommandLineSymbols().ForEach(s => rootCommand.Add(s));
        rootCommand.AddCommand(coberturaCommand);

        int res = await rootCommand.InvokeAsync(args);
        return res;
    }
}