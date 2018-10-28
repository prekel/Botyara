If ($env:APPVEYOR_REPO_TAG)
{
	dotnet publish /p:AssemblyVersion="$env:APPVEYOR_BUILD_VERSION" /p:Version="$env:APPVEYOR_REPO_TAG_NAME"
}
Else 
{
	dotnet publish /p:AssemblyVersion="$env:APPVEYOR_BUILD_VERSION"
}