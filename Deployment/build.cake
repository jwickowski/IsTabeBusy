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


Task("Run-Tests")
    .IsDependentOn("Build")
    .Does(()=>{
        XUnit2("../src/IsTableBusy/**/bin/" + configuration + "/*.Tests.dll",
        new XUnit2Settings(){Parallelism = ParallelismOption.All});
    });

Task("Default")
    .IsDependentOn("Run-Tests")
    .Does(() =>{
            Information("Hallo from cake!!!");
    });

RunTarget(target);