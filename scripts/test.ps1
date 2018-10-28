dotnet test Botyara.Core.Tests --test-adapter-path:. --logger:Appveyor


if ($env:APPVEYOR_REPO_COMMIT_MESSAGE.Contains("(deploy)")) 
{
    $env:SHOULD_DEPLOY = 'true'
}
$env:SHOULD_DEPLOY = 'true'