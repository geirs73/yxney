# Merging of multiple test projects

This is based on [Simple coverage example](../01-simple/Simple%20coverage.md)
and continues where it left off.

Copy the code from the previous example to a new directory. And run

```text
> dotnet test
```

Open `SomeMath.cs` and make sure that *Coverage Gutters* watches for code
coverage (`ctrl+shift+8`).

## Add an XUnit test project to solution

We add a XUnit project side by side with the NUnit project and add it to the
solution.

```text
> dotnet new xunit -n Foobar.XUnit.Tests

> dotnet sln Coverage.sln add Foobar.XUnit.Tests

> dotnet add Foobar.XUnit.Tests reference Foobar

> dotnet add Foobar.XUnit.Tests package coverlet.msbuild
```

## Create some additional tests in XUnit

We add some additional tests of SomeMath.cs as XUnit tests in
[`SomeMoreMathTests.cs`](Foobar.XUnit.Tests/SomeMoreMathTests.cs)

## Set up msbuild properly for building

*Everyone working with anything C# are well off by using a couple of evenings to
learn the msbuild language/system. It will make your life so much easier.*

When dotnet builds (using msbuild under the hood) it will always look for files
with certain names in the current directory and all parent directories. Files
named `Directory.Build.targets` will be automatically included after all the
content of your project file (`.csproj`).

First we go to the solution directory and create
[`Directory.Build.targets`](Directory.build.targets) and move
`LocalCleanTestCoverageFiles` there from
[Foobar.NUnit.Tests.csproj](../01-simple/Foobar.NUnit.Tests/Foobar.NUnit.Tests.csproj)

Then we create a [`Directory.Build.props`](Directory.Build.props) where we copy
the property group for setting up `coverlet.msbuild` from the test project
[`Foobar.NUnit.Tests.csproj`](../01-simple/Foobar.NUnit.Tests/Foobar.NUnit.Tests.csproj).
We also add the common for all project properties
and clean them up so they look like this: [`Foobar.csproj`](Foobar/Foobar.csproj),
[`Foobar.NUnit.Tests.csproj`](Foobar.NUnit.Tests/Foobar.NUnit.Tests.csproj) and
[`Foobar.XUnit.Tests`](Foobar.XUnit.Tests/Foobar.XUnit.Tests.csproj)

This way, all properties and targets we only had in the unit test project in the
simple example, is now valid for all three projects in this solution. They are
not needed in the Foobar project, just in the test projects, but let's keep it
as simple as possible as long as we don't get into problems.

## Change setup of coverlet to support merging

To combine the coverage result of our two test projects, we need to set up
coverlet a bit differently in `Directory.Build.props`

*Note: Usually tests projects will be of the same kind, but I just want to prove
a point here.*

I haven't tested all combinations documented by coverlet documentation, but I
have found that in the current version of dotnet (7.0 at time of writing), you
can combine the `json` format together with `cobertura` in a comma separated
list for `CoverletOutputFormat`:

```xml
<PropertyGroup>
    <CollectCoverage>true</CollectCoverage>
    <CoverletOutput>../coverage/</CoverletOutput>
    <CoverletOutputFormat>json,cobertura</CoverletOutputFormat>
    <MergeWith>../coverage/coverage.json</MergeWith>
</PropertyGroup>
```

Since we do this in `Directory.Build.props`, it will be run.

## Run merge coverage tests

On the command line run:

```text
> dotnet test
```

Then observe that `SomeMath.cs` now has increased coverage and should look like this:

![Coverage gutters screen shot](Coverage%20with%20merging.md.CoverageGutters.SomeMath.png)

Both `Divide()` and `MaxValueInArray()` are now covered.

## Notes

### 1. Update .gitignore

Remember to add the coverage/ directory to [.gitignore](../../.gitignore)

### 2. Cleanup target

There is a target named `Cleanup` in `Directory.Build.targets`, it is just for convenience.
