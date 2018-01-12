#addin "Cake.MsDeploy"

var target = Argument("target", "Default");
var siteName = Argument<string>("siteName"); //cake-istablebusy-test
var password = Argument<string>("password"); //cake-istablebusy-test

Task("Deploy")
    .Does(()=>{
    var settings = new MsDeploySettings
    {
        Source = new PackageProvider
        {
            Direction = Direction.source,
            Path = MakeAbsolute(File("./package/IsTableBusy.App.Web.zip")).ToString()
        },
      
        Verb = Operation.Sync,
        RetryAttempts = 5,
        RetryInterval = 5000,
        Destination = new AutoProvider
        {
            Direction = Direction.dest,
            AuthenticationType = AuthenticationScheme.Basic,
            ComputerName = "https://" + siteName + ".scm.azurewebsites.net:443/msdeploy.axd?site=" + siteName,
            TempAgent = false,
            Username = "$" + siteName,
            Password = password
        },
        SetParams = new List<SetParameter>
        {
            new SetParameter()
            {
                Name = "IIS Web Application Name",
                Value = siteName   
            },
            new SetParameter()
            {
                Name = "ApiUrl",
                Value = siteName + "ApiUrl - testURL"  
            },
            new SetParameter()
            {
                Name = "SignalRUrl",
                Value =  siteName + "SignalRUrl - testURL"  
            },
            new SetParameter()
            {
                Name = "DefaultConnection-Web.config Connection String",
                Value =  siteName + "databaseConnectionSstring"  
            }
        }
    };

    MsDeploy(settings);
});

Task("Default")
    .IsDependentOn("Deploy");

RunTarget(target);