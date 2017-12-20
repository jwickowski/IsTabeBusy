#tool "nuget:?package=xunit.runner.console&version=2.3.1"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var packageDir = MakeAbsolute(Directory("./packages"));

Task("Clean")
.DoesForEach(GetDirectories("../src/IsTableBusy/**/bin/" + configuration), (dir)=>{
    Information("Directory: " + dir.FullPath);   
    CleanDirectory(dir.FullPath);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
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
        XUnit2("../src/IsTableBusy/**/bin/" + configuration + "/*.Tests.dll");
    });

var webDir = packageDir + Directory("/Web/");
var webPackageDir = webDir + Directory("/package");    
Task("Clean-Web-Package-Dir")
    .Does(()=>{
        CleanDirectory(webDir);
    });

Task("Prepare-Web-Package")
    .IsDependentOn(runTestsTask)
    .IsDependentOn("Clean-Web-Package-Dir")
    .Does(()=>{
        MSBuild("../src/IsTableBusy/IsTableBusy.App.Web/IsTableBusy.App.Web.csproj",
                settings => settings
                .SetConfiguration(configuration)
                .WithTarget("Package")
                .WithProperty("PackageLocation", new string[]{ webPackageDir.ToString() })
                );
    });

Task("Copy-Deploy-Web-Script")
    .IsDependentOn("Prepare-Web-Package")
    .Does(() =>{
        CopyFiles("./deploy/Web/*", webDir.ToString());
    });

var apiDir = packageDir + Directory("/Api/");
var apiPackageDir = apiDir + Directory("/package");
Task("Clean-Api-Package-Dir")
    .Does(()=>{
        CleanDirectory(apiDir);
    });

Task("Prepare-Api-Package")
    .IsDependentOn(runTestsTask)
    .IsDependentOn("Clean-Api-Package-Dir")
    .Does(()=>{
        MSBuild("../src/IsTableBusy/IsTableBusy.App.Api/IsTableBusy.App.Api.csproj",
                settings => settings
                .SetConfiguration(configuration)
                .WithTarget("Package")
                .WithProperty("PackageLocation", new string[]{ apiPackageDir.ToString()  })
                );
    });
    
Task("Copy-Deploy-Api-Script")
    .IsDependentOn("Prepare-Api-Package")
    .Does(() =>{
        CopyFiles("./deploy/Api/*", apiDir.ToString());
    });

Task("Default")
    .IsDependentOn("Copy-Deploy-Api-Script")
    .IsDependentOn("Copy-Deploy-Web-Script");

RunTarget(target);