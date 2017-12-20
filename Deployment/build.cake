#tool "nuget:?package=xunit.runner.console&version=2.3.1"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var packageDir = MakeAbsolute(Directory("./package"));

Task("Clean")
.DoesForEach(GetDirectories("../src/IsTableBusy/**/bin/" + configuration), (dir)=>{
    Information("folder:"+  dir.FullPath);
    if(dir.Segments.Last().Equals(configuration.ToString())){
        Information("cleaning: " + dir.FullPath);   
        //CleanDirectory(dir.FullPath);
    }
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
      //  XUnit2("../src/IsTableBusy/**/bin/" + configuration + "/*.Tests.dll",
      //  new XUnit2Settings(){Parallelism = ParallelismOption.All});
    });
var webPackageDir = (packageDir + Directory("/Web")).ToString();
Task("Clean-Web-Package-Dir")
    .Does(()=>{
        CleanDirectory(webPackageDir);
    });

Task("Prepare-Web-Package")
    .IsDependentOn(runTestsTask)
    .IsDependentOn("Clean-Web-Package-Dir")
    .Does(()=>{
        MSBuild("../src/IsTableBusy/IsTableBusy.App.Web/IsTableBusy.App.Web.csproj",
                settings => settings
                .SetConfiguration(configuration)
                .WithTarget("Package")
                .WithProperty("PackageLocation", new string[]{ webPackageDir })
                );
    });

var apiPackageDir = (packageDir + Directory("/Api")).ToString();
Task("Clean-Api-Package-Dir")
    .Does(()=>{
        CleanDirectory(apiPackageDir);
    });

Task("Prepare-Api-Package")
    .IsDependentOn(runTestsTask)
    .IsDependentOn("Clean-Api-Package-Dir")
    .Does(async () => {
        MSBuild("../src/IsTableBusy/IsTableBusy.App.Api/IsTableBusy.App.Api.csproj",
                settings => settings
                .SetConfiguration(configuration)
                .WithTarget("Package")
                .WithProperty("PackageLocation", new string[]{ apiPackageDir  })
                );
    });

Task("Default")
    .IsDependentOn("Prepare-Web-Package")
    .IsDependentOn("Prepare-Api-Package");

RunTarget(target);