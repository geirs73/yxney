dotnet.exe tool restore
dotnet.exe test
dotnet.exe tool run reportgenerator -reports:".\.coverage\coverage.cobertura.xml" -targetdir:".\.coverage\htmlreport" -reporttypes:html
Invoke-Item .\.coverage\htmlreport\index.html
