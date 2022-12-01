# Simple coverage with dotnet

## Create some code to test and a solution

First we create a library we want to test, and add it to a solution you see in
this folder

```text
> dotnet new gitignore

> mkdir coverage

> dotnet new classlib -n Foobar
The template "Class Library" was created successfully.

> dotnet new sln -n Coverage

> dotnet sln Coverage.sln add Foobar
```

## Create test library

Then we create the test library. We will use just NUnit here, but XUnit is
similar and will work exactly the same. Also add reference to our `Foobar` code
in `Foobar.NUnit.Tests` project.

```text
> dotnet new nunit -n Foobar.NUnit.Tests

> dotnet sln Coverage.sln add Foobar.NUnit.Tests

> dotnet add Foobar.NUnit.Tests reference Foobar
```

The test project will already include a reference to `coverlet.collector`, but
to make things really easy, also add reference to `coverlet.msbuild`

```text
> dotnet add Foobar.NUnit.Tests\ package coverlet.msbuild
```

Then we add some simple math functions in a class file:
[SomeMath.cs](Foobar/SomeMath.cs)

## Set up coverage in csproj

We add a new property group just below the initial one with some setup for coverlet.

```xml
  <PropertyGroup>
    <CollectCoverage>true</CollectCoverage>
    <CoverletOutput>lcov.info</CoverletOutput>
    <CoverletOutputFormat>lcov</CoverletOutputFormat>
  </PropertyGroup>
```

We also need to clean up after our selves when running `dotnet clean`, we can
add a local cleaning target and hook it up to the `AfterClean` target defined by
the build system.

```xml
  <Target Name="LocalCleanTestCoverageFiles" AfterTargets="AfterClean">
    <Delete Files="lcov.info" />
  </Target>
```

Your test csproj should look like this: [Foobar.NUnit.Tests.csproj](Foobar.NUnit.Tests/Foobar.NUnit.Tests.csproj)

## Create test class

We then add a test to the SomeMathTests.cs test class

```csharp
    [Test]
    public void AddTest()
    {
        Assert.That(SomeMath.Add(5, 4), Is.EqualTo(9));
    }
```

And then we run the tests

```text
> dotnet test
```

## Show coverage in editor gutters

Install *Coverage Gutters* vscode extension.

Go to the `SomeMath.cs` file open the command window (`ctrl+shift+p`) and type 'gutter' to see the options.

* Display - show result once, will not update when you run new coverage test. Key: `ctrl+shift+7`
* Watch - look for updates to coverage continuously and display it  Key: `ctrl+shift+8`
* Remove Watch - as it says. Key: `ctrl+shift+9`
* Clear display. Key: `ctrl+backspace`

![image.png](Simple%20coverage.md.CoverageGutters.png)

## Watch increased coverage

Start the coverage watch in *Coverage Gutters* with `ctrl+shift+8`

Now, add a new test in `SomeMathTests.cs`

```csharp
    [Test]
    public void EvenNumbersInArrayTest()
    {
        Assert.That(SomeMath.EvenNumbersInArray(new int[] { 3, 3, 5, 1, 9 }), Is.EqualTo(new int[] { }));
    }
```

Here is the [`SomeMathTests.cs`](Foobar.NUnit.Tests/SomeMathTests.cs) after we're done.

Run tests again:

```text
> dotnet test
```

Your editor should now look something like this:

![Example of how gutters look like](Simple%20coverage.md.CoverageGutters.SomeMath.png)

## Notes

### Configuration of Coverage Gutters extension

Coverage Gutters has just a few file names it will look for. If you do not set
the name of the output file like we did above, it will not find anything. You
can add new files to look fore in vscode settings file.

```json
    "coverage-gutters.coverageFileNames": [
        "lcov.info",
        "cov.xml",
        "coverage.xml",
        "jacoco.xml",
        "coverage.cobertura.xml",        
    ],
```

### .gitignore

Remember to add your coverage files to .gitignore unless they are there already.

**[Go to next example](../02-merge/Coverage%20with%20merging.md)**
