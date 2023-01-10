<#
.SYNOPSIS
Get parameter aliases for $Command
.DESCRIPTION
Lists all parameter aliases having aliases for a specific $Command. Can show all parameters using switch $All
#>
function Get-CommandParameterAliases {

    [CmdletBinding()]
    param (
        
        #The command to get parameter aliases for
        $Command,

        # Include parameters that not have been aliased
        [switch] $IncludeNonAliased,

        # Include common cmdlet parameters like Verbose and InformationAction
        [switch] $IncludeCommon
    )

    $excludeParams = @(
        'Debug', 'ErrorAction', 'ErrorVariable', 'InformationAction', 'InformationVariable',
        'OutVariable', 'OutBuffer', 'PipelineVariable', 'Verbose', 'WarningAction', 'WarningVariable'
    )

    $params = (Get-Command $Command).Parameters.Values 

    if (!$IncludeCommon) { $params = $params | Where-Object { $excludeParams -notcontains $_.Name } }

    if ($IncludeNonAliased) { $params = $params | Format-Table -Property Name, Aliases }
    else { $params = $params | Where-Object { $_.Aliases.Count -gt 0 } }

    $params | Format-Table -Property Name, Aliases
}

