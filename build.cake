#tool gitlink

var target = Argument("target", "package");
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
	NuGetRestore("./src/lib.sln");
	DotNetBuild("./src/lib.sln", x => x
        .SetConfiguration("Release")
        .SetVerbosity(Verbosity.Minimal)
        .WithTarget("build")
        .WithProperty("Platform", "Any CPU")
        .WithProperty("TreatWarningsAsErrors", "false")
    );
});


Task("package")
	.IsDependentOn("build")
	.Does(() =>
{
    /*
    GitLink("./", new GitLinkSettings
    {
         RepositoryUrl = "https://github.com/aritchie/deviceinfo",
         Branch = "master"
    });
    */
    NuGetPack(new FilePath("./nuspec/Acr.DeviceInfo.nuspec"), new NuGetPackSettings());
	MoveFiles("./*.nupkg", "./output");
});

Task("publish")
    .IsDependentOn("package")
    .Does(() =>
{
    NuGetPush("./output/*.nupkg", new NuGetPushSettings
    {
        Source = "http://www.nuget.org/api/v2/package",
        Verbosity = NuGetVerbosity.Detailed
    });
    CopyFiles("./output/*.nupkg", "c:\\users\\allan.ritchie\\dropbox\\nuget");
});

RunTarget(target);