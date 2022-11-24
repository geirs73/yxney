# Merging of multiple test projects

Please see [Simple Coverage first](../coverage-simple/Simple%20coverage.md)

## Add an XUnit test project

We add a XUnit project side by side with the NUnit project 

```text

> dotnet new xunit -n Foobar.XUnit.Tests

> dotnet sln .\Coverage.sln add .\Foobar.XUnit.Tests\

> dotnet add .\Foobar.XUnit.Tests\ reference .\Foobar\  

> dotnet add .\Foobar.XUnit.Tests\ package coverlet.msbuild
```

fdasf.
