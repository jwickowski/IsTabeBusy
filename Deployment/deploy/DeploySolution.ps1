function DeploySolution(){
param($NodeName, $Environment, $Tokens)



$packagesPath = (Get-ConfigurationPaths).PackagesPath
    $msDeployParams = @{ PackageName = 'IsTableBusy'
                         PackageType = 'Web'
                         PackagePath = "$packagesPath\IsTableBusy\IsTableBusy.zip"
                         Node = $NodeName
                         Environment = $Environment
                         MsDeployDestinationString = $Tokens.Remoting.MsDeployDestination
                         #TokensForConfigFiles = $Tokens.DiagnosticWebConfig
                         #Website = "IsTableBusyWebSite"#$Tokens.WebDeployConfig.DiagnosticWebsiteName
                         #WebApplication ="IsTableBusyWebApp"# $Tokens.WebDeployConfig.DiagnosticApplicationName
                         TokenUpdateMode = 'SetParam'
                         SkipDir = 'Logs'
                         
					   }

    Deploy-MsDeployPackage @msDeployParams 

}