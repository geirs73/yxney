# Coverage reports

Please see [Simple Coverage first](../01-simple/Simple%20coverage.md)

```text
> dotnet new tool-manifest
> dotnet tool install dotnet-reportgenerator-globaltool
```

`Dotnet.Solution.targets`
```xml
<Project>
  <ItemGroup />
  <Target Name="Coverage">    
    <Exec Command='dotnet tool run reportgenerator -reports:".\.coverage\coverage.cobertura.xml" -targetdir:".\.coverage\report.html" -reporttypes:html' />
  </Target>

</Project>
```

fdasf.
