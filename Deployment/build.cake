var target = Argument("target", "Default");

Task("Build-Api")
    .Does(()=>{
        MSBuild("../App/IsTableBusy.sln");
    });

Task("Default")
    .IsDependentOn("Build-Api")
    .Does(() =>{
            Information("Hallo from cake!!!");
    });

RunTarget(target);