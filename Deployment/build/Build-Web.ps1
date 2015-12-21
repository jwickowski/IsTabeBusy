function Build-Web(){
New-MsBuildOptions
    $path = Get-ConfigurationPaths
    $projectApiPath = Join-Path $path.ProjectRootPath -ChildPath 'app/IsTableBusy.App.Web/IsTableBusy.App.Web.csproj'
    $outputPath = Join-Path $path.PackagesPath -ChildPath 'Web'

    Build-WebPackage  -PackageName "web" `
                      -ProjectPath $projectApiPath `
                      -OutputPath $outputPath 
}