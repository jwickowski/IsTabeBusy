#tool "nuget:?package=xunit.runner.console&version=2.3.1"

var target = Argument<string>("target", "Default");
var configuration = Argument<string>("configuration", "Release");
var integrationTestConnectionString =Argument<string>("integrationTestConnectionString", null);
var packageDir = MakeAbsolute(Directory("./packages"));
var tempPackageDir = MakeAbsolute(Directory("./tmpPackages"));


Task("Clean")
    .DoesForEach(GetDirectories("../src/IsTableBusy/**/bin/" + configuration), (dir)=>{
        Information("Cleaning directory: " + dir.FullPath);   
        CleanDirectory(dir.FullPath);
});

Task("Clean-Package-Dir")
    .Does(()=>{
        Information("Cleaning directory: " + packageDir);   
        CleanDirectory(packageDir);

        Information("Cleaning directory: " + tempPackageDir);   
        CleanDirectory(tempPackageDir);
    });

Task("Restore-NuGet-Packages")
   .Does(()=>{
        NuGetRestore("../src/IsTableBusy/IsTableBusy.sln");
    });

Task("Build")
    .Does(()=>{
        MSBuild("../src/IsTableBusy/IsTableBusy.sln",
            settings => settings
                .SetConfiguration(configuration)
                .WithProperty("PackageLocation", new string[]{ tempPackageDir.ToString() })
                .WithProperty("WebPublishMethod", "Package")
                .WithProperty("PackageAsSingleFile", "true")
                .WithProperty("DeployOnBuild", "true")
                );
    });

Task("Update-ConnectionString-For-Integration-Tests")
    .Does(()=> {
        if(string.IsNullOrEmpty(integrationTestConnectionString)){
            Information("Argument 'integrationTestConnectionString' is empty. Settings won't be upated.");
            return;
        }
        var dir  = Directory($"../src/IsTableBusy/IsTableBusy.Core.IntegrationTests/bin/{configuration}/");
        var absolutePath = MakeAbsolute(dir) + "/IsTableBusy.Core.IntegrationTests.dll.config";
        var settingsFile = File(absolutePath);
        var xPath = "/configuration/connectionStrings/add[@ name = 'DefaultConnection']/@connectionString";
        Information($"Updating file '{absolutePath}' with '{integrationTestConnectionString}'");
        
        XmlPoke(settingsFile, xPath, integrationTestConnectionString);
    });

Task("Run-Integration-Tests") 
    .Does(()=>{
        XUnit2("../src/IsTableBusy/**/bin/" + configuration + "/*.IntegrationTests.dll", 
            new XUnit2Settings()
            {
                HtmlReport = true,
                XmlReport = true,
                OutputDirectory = packageDir + Directory("/TestResults/")
            });
    });

Task("Copy-Packages")
    .Does(()=>{
        var apiPackagePath = packageDir + "/Api";
        Information("Creating directory: " + apiPackagePath); 
        CreateDirectory(apiPackagePath);
        
        var apiPackageFileName = "IsTableBusy.App.Api.zip";
        var apiFiles = new [] {
            tempPackageDir + "/IsTableBusy.App.Api.zip",
            tempPackageDir + "/IsTableBusy.App.Api.SetParameters.xml"
        };

        Information("CreatCopying files to: " + apiPackagePath); 
        CopyFiles(apiFiles, apiPackagePath);


         var webPackagePath = packageDir + "/Web";
        Information("Creating directory: " + webPackagePath); 
        CreateDirectory(webPackagePath);
        
        var webPackageFileName = "IsTableBusy.App.Web.zip";
        var webFiles = new [] {
            tempPackageDir + "/IsTableBusy.App.Web.zip",
            tempPackageDir + "/IsTableBusy.App.Web.SetParameters.xml"
        };
        
        Information("CreatCopying files to: " + webPackagePath); 
        CopyFiles(webFiles, webPackagePath);
    });

Task("Copy-Deploy-Scripts")
    .Does(()=>{
        CopyFiles("./deployScripts/*", packageDir.ToString());
		
		var packageToolsPath = packageDir + "/Tools";
		Information("Creating directory: " + packageToolsPath); 
		CreateDirectory(packageToolsPath);
		
		CopyFile("./tools/packages.config", packageToolsPath + "/packages.config");
    });
    

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Clean-Package-Dir")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Build")
    //.IsDependentOn("Update-ConnectionString-For-Integration-Tests")
    //.IsDependentOn("Run-Integration-Tests")

    .IsDependentOn("Copy-Packages")
    .IsDependentOn("Copy-Deploy-Scripts");

RunTarget(target);