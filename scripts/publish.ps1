dotnet publish


if ($env:APPVEYOR_REPO_COMMIT_MESSAGE.Contains("(deploy)")) 
{
    $env:SHOULD_DEPLOY = 'true'
}
$env:SHOULD_DEPLOY = 'true'