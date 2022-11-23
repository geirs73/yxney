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

> dotnet sln .\Coverage.sln add .\Foobar\
```

## Create test library

Then we create the test library. We will use just NUnit here, but XUnit is
similar and will work exactly the same. Also add reference to our `Foobar` code
in `Foobar.NUnit.Tests` project.

```text
> dotnet new nunit -n Foobar.NUnit.Tests

> dotnet sln .\Coverage.sln add .\Foobar.NUnit.Tests\

> dotnet add .\Foobar.NUnit.Tests\ reference .\Foobar\
```

The test project will already include a reference to `coverlet.collector`, but
to make things really easy, also add reference to `coverlet.msbuild`

```text
> dotnet add .\Foobar.NUnit.Tests\ package coverlet.msbuild
```

## Add target for coverage in csproj

Then we need to add a local build target and hook it up to run before VSTest
target (that is invoked by calling dotnet test). This is
`Foobar.NUnit.Tests.csproj` after we have changed it.

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net7.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>

    <IsPackable>false</IsPackable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="coverlet.msbuild" Version="3.2.0">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.3.2" />
    <PackageReference Include="NUnit" Version="3.13.3" />
    <PackageReference Include="NUnit3TestAdapter" Version="4.2.1" />
    <PackageReference Include="NUnit.Analyzers" Version="3.3.0" />
    <PackageReference Include="coverlet.collector" Version="3.1.2" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\Foobar\Foobar.csproj" />
  </ItemGroup>

  <!-- This is all it takes to produce a coverage file that can be picked
  by coverage extensions or other tools -->
  <Target Name="BeforeTestCoverage" BeforeTargets="VSTest" Condition="'$(Configuration)'=='Debug'">
    <PropertyGroup>
      <CollectCoverage>true</CollectCoverage>
      <CoverletOutput>lcov.info</CoverletOutput>
      <CoverletOutputFormat>lcov</CoverletOutputFormat>
    </PropertyGroup>
  </Target>

</Project>
```

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

Install 'Coverage Gutters' vscode extension.

Go to the `SomeMath.cs` file open the command window (CTRL+SHIFT+P) and type 'gutter' to see the options.

* Display - show result once, will not update when you run new coverage test
* Watch - look for updates to coverage continuously and display it (you probably
  want this for a while)
* Remove Watch - as it says

![image.png](Simple%20coverage.md.CoverageGutters.png)

Start the wathcing of coverage (CTRL+SHIFT+8 usually)

Now, add a new test and see the coverage change when running tests again.

```csharp
    [Test]
    public void EvenNumbersInArrayTest()
    {
        Assert.That(SomeMath.EvenNumbersInArray(new int[] { 3, 3, 5, 1, 9 }), Is.EqualTo(new int[] { }));
    }
```

Your editor should now look something like this:

![Example of how gutters look like](Simple%20coverage.md.CoverageGutters.SomeMath.png)

## Note 1

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

I prefer to use "coverage*." names for coverage files, and update my json
settings file accordingly. Those are the filenames that `dotnet new gitignore`
lists when you create a new ignore file.

## Note 2

I'm a fan of letting clean do proper cleaning, so I've added a
`LocalCleanTestCoverage` target that hooks into `AfterClean` which removes the
lcov.info file we made if running `dotnet clean` on the solution.
