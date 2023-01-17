param ()

Set-StrictMode -Version Latest

$Functions = @(Get-ChildItem -Path $PSScriptRoot\Scripts\*.ps1 -ErrorAction SilentlyContinue)

foreach($import in $Functions)
{
    try {
        . $import.FullName
    } catch {
        Write-Error "Failed to import function $($import.FullName): $_"
    }
}

# $exportList = @{
#     Function = @(
#         'Get-DirectoryListing',
#         'Get-CommandParameterAliases'
#     )
#     Variable = @{

#     }
# }

Export-ModuleMember -Function * -Cmdlet * -Alias *