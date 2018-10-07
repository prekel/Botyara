$env:need_deploy = $env:APPVEYOR_REPO_COMMIT_MESSAGE.Contains("(deploy)")

$env:rev = git rev-list HEAD --count
$env:assembly_version = "$env:APPVEYOR_BUILD_VERSION.$env:rev"
Write-Host -Backgroundcolor DarkGreen -Foregroundcolor White "Assembly Version: $env:assembly_version"