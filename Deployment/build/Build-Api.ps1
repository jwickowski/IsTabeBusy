function Build-Api(){
New-MsBuildOptions
    $path = Get-ConfigurationPaths
    $projectApiPath = Join-Path $path.ProjectRootPath -ChildPath 'app/IsTableBusy.App.Api/IsTableBusy.App.Api.csproj'
    $outputPath = Join-Path $path.PackagesPath -ChildPath 'Api'

    Build-WebPackage  -PackageName "Api" `
                      -ProjectPath $projectApiPath `
                      -OutputPath $outputPath 
}