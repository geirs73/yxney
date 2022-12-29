using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Yxney.IO.HashPath;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class DirectoryLengths : IEnumerable<int>
{
    private readonly int[] _directoryLengths;
    private readonly string _toString;
    private readonly int _totalLength;

    public static DirectoryLengths Create(int firstLevel)
    {
        return new DirectoryLengths(firstLevel);
    }

    public static DirectoryLengths Create(int firstLevel, int secondLevel)
    {
        return new DirectoryLengths(firstLevel, secondLevel);
    }

    public static DirectoryLengths Create(int firstLevel, int secondLevel, int thirdLevel)
    {
        return new DirectoryLengths(firstLevel, secondLevel, thirdLevel);
    }

    public static DirectoryLengths Create(int firstLevel, int secondLevel, int thirdLevel, int fourthLevel)
    {
        return new DirectoryLengths(firstLevel, secondLevel, thirdLevel, fourthLevel);
    }

    public static DirectoryLengths Create(int firstLevel, int secondLevel, int thirdLevel, int fourthLevel, params int[] nextLevels)
    {
        List<int> levels = new() { firstLevel, secondLevel, thirdLevel, fourthLevel };
        levels.AddRange(nextLevels);
        return new DirectoryLengths(levels.ToArray());
    }

    public static DirectoryLengths Create()
    {
        return new DirectoryLengths();
    }

    private DirectoryLengths()
        : this(new int[] { 3 })
    {
    }

    private DirectoryLengths(params int[] levelLengths)
    {
        _directoryLengths = levelLengths;
        _toString = string.Join(",", _directoryLengths);
        _totalLength = _directoryLengths.Sum();
    }

    public int TotalLength()
    {
        return _directoryLengths.Sum(x => x);
    }

    public IEnumerator<int> GetEnumerator()
    {
        return _directoryLengths.ToList().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string? ToString() => _toString;
    private string AsString() => _toString;

    [ExcludeFromCodeCoverage]
    private string DebuggerDisplay => $"DirectoryLengths: {_toString} ({_totalLength:0})";

    public static implicit operator string(DirectoryLengths directoryLengths)
    {
        ArgumentNullException.ThrowIfNull(directoryLengths);
        return directoryLengths.AsString();
    }
}
