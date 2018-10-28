dotnet build /p:AssemblyVersion="$env:APPVEYOR_BUILD_VERSION" /verbosity:minimal /m


if ($env:APPVEYOR_REPO_COMMIT_MESSAGE.Contains("(deploy)")) 
{
    $env:SHOULD_DEPLOY = 'true'
}
$env:SHOULD_DEPLOY = 'true'