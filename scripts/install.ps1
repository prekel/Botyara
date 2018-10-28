$env:rev = git rev-list HEAD --count
$env:assembly_version = "$env:APPVEYOR_BUILD_VERSION.$env:rev"
$env:APPVEYOR_BUILD_VERSION = $env:assembly_version

Update-AppveyorBuild -Version "$env:assembly_version"

Write-Host -Backgroundcolor DarkGreen -Foregroundcolor White "Assembly Version: $env:assembly_version"
Write-Host -Backgroundcolor DarkGreen -Foregroundcolor White "Assembly Version: $env:APPVEYOR_BUILD_VERSION"