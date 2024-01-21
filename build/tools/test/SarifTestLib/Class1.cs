using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SarifTestLib;

public class Class1
{
    private Foobars foobars;
    private Foobars _zotzot;

    void DoSomething(Foobars foobars)
    {
        
    }

    public record Zotbar(string[] PublicStringArray);

    List<int> myList = [1, 2, 3, 4];
    var reversedList = myList.Select(i => i).Reverse();
}

internal enum Foobars
{
    Bar,
    Foo, 
    Zot
};
