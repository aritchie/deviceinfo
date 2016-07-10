//#tool XamarinComponent
//#addin Cake.Xamarin
#addin Cake.FileHelpers
#tool nunit.consolerunner
#tool gitlink

var target = Argument("target", "publish");
var version = Argument("version", "1.0.0");

Setup(x =>
{
    DeleteFiles("./*.nupkg");
    DeleteFiles("./output/*.*");

	if (!DirectoryExists("./output"))
		CreateDirectory("./output");
});

Task("build")
	.Does(() =>
{
	NuGetRestore("./Acr.DeviceInfo.sln");
	DotNetBuild("./Acr.DeviceInfo.sln", x => x
        .SetConfiguration("Release")
        .SetVerbosity(Verbosity.Minimal)
        .WithTarget("build")
        .WithProperty("TreatWarningsAsErrors", "false")
    );
});


Task("nuget")
	.IsDependentOn("build")
	.Does(() =>
{
    NuGetPack(new FilePath("./nuspec/Acr.DeviceInfo.nuspec"), new NuGetPackSettings());
	MoveFiles("./*.nupkg", "./output");
});

Task("publish")
    .IsDependentOn("nuget")
    .Does(() =>
{
    GitLink("./", new GitLinkSettings
    {
         RepositoryUrl = "https://github.com/aritchie/deviceinfo",
         Branch = "master"
    });
    NuGetPush("./output/*.nupkg", new NuGetPushSettings
    {
        Source = "http://www.nuget.org/api/v2/package",
        Verbosity = NuGetVerbosity.Detailed
    });
    CopyFiles("./output/*.nupkg", "c:\\users\\allan.ritchie\\dropbox\\nuget");
});

RunTarget(target);