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

<#
.SYNOPSIS
List entries in directory
.DESCRIPTION
Displays directories much like ls(1) on unix, using Get-ChildItem
#>
function Get-DirectoryListing {

    [CmdletBinding()]
    Param (

        # Alias: -a  Show all files and other entries
        [Alias('a')]
        [switch] $All,

        [Alias('dd')]
        # Alias: -dd  Show only directories/containers
        [switch] $Directory,
        
        [Alias('ff')]
        # Alias: -ff  Show only files
        [switch] $File,

        # Alias -g  Group directories first
        [Alias('g')]
        [switch] $DirectoriesFirst,

        # Alias: -l  Long list format (dir equivalent)
        [Alias('l')]
        [switch] $LongListingFormat,

        # Alias: -r  Reverse order while sorting
        [Alias('r')]
        [switch] $Reverse,

        # Alias: -s  Sort by file size, largest first
        [Alias('s')]
        [switch] $SortByFileSize,

        # Alias: -t  Sort by time, newest first
        [Alias('t')]
        [switch] $SortByLastModified,

        # Alias: -x  Sort alphabetically by extension
        [Alias('x')]
        [switch] $SortByExtension,

        # Get help on this command
        [switch] $Help,

        # Path
        [System.IO.DirectoryInfo] $Path = '.'
    )

    if ($Help) {
        Get-Help -Detailed Get-DirectoryListing
        return
    }

    [string] $force = $(if ($All) { '-Force' })
    [string] $onlyDir = $(if ($Directory) { '-Directory' })
    [string] $onlyFile = $(if ($File) { '-File' })

    [string] $exp = "Get-ChildItem $force $onlyDir $onlyFile"

    if ($Path -and $Path.Exists) {
        $exp += " -Path $Path"
    }
    else {
        Write-Error "Path not valid"
        return
    }

    # Dangerous, but we check that $Path is an existing Directory first, so it
    # should be safe
    $items = Invoke-Expression $exp | Sort-Object -Property Name

    if ($LongListingFormat) {
        if ($Reverse) { [array]::Reverse($items) }
        if ($SortByLastModified) { $items = $items | Sort-Object -Descending -Property LastWriteTime }
        if ($SortByFileSize) { $items = $items | Sort-Object -Descending -Property Length }
        if ($SortByExtension) { $items = $items | Sort-Object -Property Extension }
        if ($DirectoriesFirst) { $items = $items | Sort-Object -Descending -Property PSIsContainer }
        $items | Format-Table
        return
    }

    if ($SortByLastModified) { $items = $items | Sort-Object -Descending -Property LastWriteTime }
    if ($SortByFileSize) { $items = $items | Sort-Object -Descending -Property Length }
    if ($SortByExtension) { $items = $items | Sort-Object -Property Extension }
    if ($DirectoriesFirst) { $items = $items | Sort-Object -Descending -Property PSIsContainer }
    if ($Reverse) { [array]::Reverse($items) }

    $maxLength = 1
    foreach ($item in $items) {
        if ($item.Name.Length -gt $maxLength) { $maxLength = $item.Name.Length }
    }

    [int] $width = $HOST.UI.RawUI.WindowSize.Width

    [int] $paddingFactor = 2

    # Calculate number of characters available for a column by using window size
    # and max length of all of the entries. Need to add space for Terminal-Icons
    # by adding 2 chars for each column

    if (Get-Command -Name Format-TerminalIcons -Module "Terminal-Icons" -ErrorAction SilentlyContinue) {
        $paddingFactor = 4
    }

    [int] $columns = [int][System.Math]::Floor($width / ($maxLength + $paddingFactor))

    if ($columns -eq 0) { $columns = 1 }

    # Calculate new SortValue property to make Format-Wide present resulting
    # table transposed, otherwise, it will be really hard to use. We also need
    # to take into account that the last row may not be complete and count
    # properly.
    [int] $rows = [int][System.Math]::Ceiling($items.Length / $columns)
    [int] $lastRow = $columns - ($columns * $rows - ($items.Length))

    [int] $index = 0
    for ($i = 0; $i -lt $columns; $i++) {
        [int] $colRows = $rows
        if ($i -ge $lastRow) { $colRows-- }
        for ($j = 0; $j -lt $colRows; $j++) {
            $item = $items[$index]
            $item | Add-Member -NotePropertyName SortValue -NotePropertyValue ($j * $columns + $i)
            $index++
        }
    }
    $items | Sort-Object -Property SortValue | Format-Wide -Column $columns
}



