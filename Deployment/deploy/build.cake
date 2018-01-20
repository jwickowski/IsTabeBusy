#addin "Cake.MsDeploy"

var target = Argument("target", "Default");
var webSiteName = Argument<string>("webSiteName");
var webSitePassword = Argument<string>("webSitePassword");
var apiSiteName = Argument<string>("apiSiteName");
var apiSitePassword = Argument<string>("apiSitePassword");
var dbServer = Argument<string>("dbServer");
var dbInitialCatalog = Argument<string>("dbInitialCatalog");
var dbUserID = Argument<string>("dbUserID");
var dbPassword = Argument<string>("dbPassword");

var connectionString = $"Server={dbServer};Initial Catalog={dbInitialCatalog};Persist Security Info=False;User ID={dbUserID};Password={dbPassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

Task("DeployApiApp")
    .Does(()=>{
        var apiPackagePath = MakeAbsolute(File("./Api/IsTableBusy.App.Api.zip")).ToString();
        var parameters = new List<SetParameter>
        {
            new SetParameter()
            {
                Name = "IIS Web Application Name",
                Value = siteName   
            },
            new SetParameter()
            {
                Name = "DefaultConnection-Web.config Connection String",
                Value =  connectionString  
            }
        }
        DeployToAzure(apiPackagePath, apiSiteName, apiSitePassword, parameters);
});

Task("DeployWebApp")
    .Does(()=>{
        var webPackagePath = MakeAbsolute(File("./Web/IsTableBusy.App.Web.zip")).ToString();
        var apiUrl = $"http://{apiSiteName}.azurewebsites.net/";
        var signalRUrl = $"http://{apiSiteName}.azurewebsites.net/signalr";
        var parameters = new List<SetParameter>
        {
            new SetParameter()
            {
                Name = "IIS Web Application Name",
                Value = siteName   
            },
            new SetParameter()
            {
                Name = "ApiUrl",
                Value = apiUrl 
            },
            new SetParameter()
            {
                Name = "SignalRUrl",
                Value =  signalRUrl
            },
            new SetParameter()
            {
                Name = "DefaultConnection-Web.config Connection String",
                Value =  connectionString  
            }
        }
        DeployToAzure(webPackagePath, webSiteName, webSitePassword, parameters);
});

Task("Default")
    .IsDependentOn("DeployApiApp");
    .IsDependentOn("DeployWebApp");

RunTarget(target);

private static void  DeployToAzure(string packagePath, string siteName, string sitePassword,List<SetParameter> parameters){
var settings = new MsDeploySettings
    {
        Source = new PackageProvider
        {
            Direction = Direction.source,
            Path = packagePath
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
        SetParams = parameters
    };

    MsDeploy(settings);
}