using System.CommandLine;
using CommandStructureBuilder.Binders;

var copyFileCmdHandler = new CopyFileCommandHandler();
var rootCmdHandler = new RootCommandHandler();
rootCmdHandler.RootCommand.AddCommand(copyFileCmdHandler.Command);

return await rootCmdHandler.RootCommand.InvokeAsync(args);