#tool "nuget:?package=xunit.runner.console&version=2.3.1"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var packageDir = MakeAbsolute(Directory("./package"));

Task("Restore-NuGet-Packages")
    .Does(()=>{
        NuGetRestore("../src/IsTableBusy/IsTableBusy.sln");
    });

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(()=>{
        MSBuild(
            "../src/IsTableBusy/IsTableBusy.sln",
         settings => settings.SetConfiguration(configuration)
        );
    });

var runTestsTask = Task("Run-Tests")
    .IsDependentOn("Build")
    .Does(()=>{
      //  XUnit2("../src/IsTableBusy/**/bin/" + configuration + "/*.Tests.dll",
      //  new XUnit2Settings(){Parallelism = ParallelismOption.All});
    });

Task("Prepare-Web-Package")
    .IsDependentOn(runTestsTask)
    .Does(()=>{
        MSBuild("../src/IsTableBusy/IsTableBusy.App.Web/IsTableBusy.App.Web.csproj",
                settings => settings
                .SetConfiguration(configuration)
                .WithTarget("Package")
                .WithProperty("PackageLocation", new string[]{ (packageDir + Directory("/Web")).ToString()  })
                );
    });

Task("Prepare-Api-Package")
    .IsDependentOn(runTestsTask)
    .Does(()=>{
  MSBuild("../src/IsTableBusy/IsTableBusy.App.Api/IsTableBusy.App.Api.csproj",
                settings => settings
                .SetConfiguration(configuration)
                .WithTarget("Package")
                .WithProperty("PackageLocation", new string[]{ (packageDir + Directory("/Api")).ToString()  })
                );
    });

Task("Default")
.IsDependentOn("Prepare-Web-Package")
.IsDependentOn("Prepare-Api-Package");

RunTarget(target);