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
   .Does(()=>{
        NuGetRestore("../src/IsTableBusy/IsTableBusy.sln");
    });

Task("Build")
    .Does(()=>{
        MSBuild(
            "../src/IsTableBusy/IsTableBusy.sln",
         settings => settings.SetConfiguration(configuration)
        );
    });

Task("Run-Tests") 
    .Does(()=>{
        XUnit2("../src/IsTableBusy/**/bin/" + configuration + "/*.Tests.dll", 
            new XUnit2Settings()
            {
                HtmlReport = true,
                XmlReport = true,
                OutputDirectory = packageDir + Directory("/TestResults/")
            });
    });

Task("Clean-Package-Dir")
    .Does(()=>{
        CleanDirectory(packageDir);
    });

Task("Prepare-Web-Package")
    .Does(()=>{
        var webPackageDir = MakeAbsolute(Directory("./packages/Web")).ToString();    
        MSBuild("../src/IsTableBusy/IsTableBusy.App.Web/IsTableBusy.App.Web.csproj",
                settings => settings
                .SetConfiguration(configuration)
                .WithTarget("Package")
                .WithProperty("PackageLocation", new string[]{ webPackageDir.ToString() })
                );
    });

Task("Prepare-Api-Package")
    .Does(()=>{
        var apiPackageDir = MakeAbsolute(Directory("./packages/Api")).ToString();
        MSBuild("../src/IsTableBusy/IsTableBusy.App.Api/IsTableBusy.App.Api.csproj",
                settings => settings
                .SetConfiguration(configuration)
                .WithTarget("Package")
                .WithProperty("PackageLocation", new string[]{ apiPackageDir.ToString()  })
                );
    });

Task("Copy-Deploy-Scripts")
    .Does(()=>{
        CopyFiles("./deploy/*", packageDir.ToString());
    });
    

Task("Default")
    .IsDependentOn("Clean")
    //.IsDependentOn("Restore-NuGet-Packages")
    //.IsDependentOn("Build")
    //.IsDependentOn("Run-Tests")
    .IsDependentOn("Clean-Package-Dir")
    .IsDependentOn("Prepare-Api-Package")
    .IsDependentOn("Prepare-Web-Package")
    .IsDependentOn("Copy-Deploy-Scripts");

RunTarget(target);