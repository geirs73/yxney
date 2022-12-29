using System.Collections.Immutable;
using System.IO;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Yxney.IO.HashPath;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class DirectoryStructure : IEnumerable<int>
{
    private readonly int[] _directoryStructure;
    private readonly string _toString;
    private readonly int _totalLength;

    public static DirectoryStructure Create(int firstLevel)
    {
        return new DirectoryStructure(firstLevel);
    }

    public static DirectoryStructure Create(int firstLevel, int secondLevel)
    {
        return new DirectoryStructure(firstLevel, secondLevel);
    }

    public static DirectoryStructure Create(int firstLevel, int secondLevel, int thirdLevel)
    {
        return new DirectoryStructure(firstLevel, secondLevel, thirdLevel);
    }

    public static DirectoryStructure Create(int firstLevel, int secondLevel, int thirdLevel, int fourthLevel)
    {
        return new DirectoryStructure(firstLevel, secondLevel, thirdLevel, fourthLevel);
    }

    public static DirectoryStructure Create(int firstLevel, int secondLevel, int thirdLevel, int fourthLevel, params int[] nextLevels)
    {
        List<int> levels = new() { firstLevel, secondLevel, thirdLevel, fourthLevel };
        levels.AddRange(nextLevels);
        return new DirectoryStructure(levels.ToArray());
    }

    public static DirectoryStructure Create()
    {
        return new DirectoryStructure();
    }

    private DirectoryStructure()
        : this(new int[] { 3 })
    {
    }

    private DirectoryStructure(params int[] levelLengths)
    {
        _directoryStructure = levelLengths;
        _toString = string.Join(",", _directoryStructure);
        _totalLength = _directoryStructure.Sum();
    }

    public int TotalLength()
    {
        return _directoryStructure.Sum(x => x);
    }

    public IEnumerator<int> GetEnumerator()
    {
        return _directoryStructure.ToList().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string? ToString() => _toString;
    private string AsString() => _toString;

    [ExcludeFromCodeCoverage]
    private string DebuggerDisplay => $"{nameof(DirectoryStructure)}: {_toString} ({_totalLength:0})";

    public static implicit operator string(DirectoryStructure structure)
    {
        ArgumentNullException.ThrowIfNull(structure);
        return structure.AsString();
    }
}
