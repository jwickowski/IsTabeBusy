var target = Argument("target", "Default");

Task("Restore-NuGet-Packages")
    .Does(()=>{
        NuGetRestore("../src/IsTableBusy/IsTableBusy.sln");
    });

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(()=>{
        MSBuild("../src/IsTableBusy/IsTableBusy.sln");
    });

Task("Default")
    .IsDependentOn("Build")
    .Does(() =>{
            Information("Hallo from cake!!!");
    });

RunTarget(target);