If ($env:APPVEYOR_BUILD_WORKER_IMAGE -eq "Visual Studio 2017")
{
	$env:rev = git rev-list HEAD --count
	$env:assembly_version = "$env:APPVEYOR_BUILD_VERSION.$env:rev"
	Update-AppveyorBuild -Version "$env:assembly_version"
}

if ($env:APPVEYOR_REPO_COMMIT_MESSAGE.Contains("(deploy)")) 
{
    $env:SHOULD_DEPLOY = 'true'
}
$env:SHOULD_DEPLOY = 'true'

Write-Host -Backgroundcolor DarkGreen -Foregroundcolor White "Assembly Version: $env:APPVEYOR_BUILD_VERSION"