# Coverage reports

This is based on [Coverage with merging example](../02-merge/Coverage%20with%20merging.md) and continues where it left off.

## ReportGenerator

We can add a report generator tool as a local tool to create html reports, to be able to find code without having to browse through all the classes in vscode. This tool is available as a dotnet tool, and even though it has global in its name, it works as a local tool too.

```text
> dotnet new tool-manifest
> dotnet tool install dotnet-reportgenerator-globaltool
```

The we just need a small powershell script to restore the report tool, perform the tests and invoke a browser with the report.
[Coverage.ps1](Coverage.ps1)
