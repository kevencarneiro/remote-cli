///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var solutionPath = Directory("./../src/RemoteCli.Client");

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
   // Executed BEFORE the first task.
   Information("Running tasks...");
   DeleteDirectory("./artifacts", true);
});

Teardown(ctx =>
{
   // Executed AFTER the last task.
   Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Default")
.Does(() => {
   Information("Hello Cake!");
});

Task("Build")
.Does(() => {
    var settings = new DotNetCorePublishSettings
    {
        Framework = "netcoreapp2.1",
        OutputDirectory = "./artifacts/",
        Runtime = "win7-x86",
        SelfContained = true
    };
    DotNetCorePublish(solutionPath, settings);
});

Task("Pack")
.IsDependentOn("Build")
.Does(() => {
    InnoSetup(new FilePath("./remotecli-agent.iss"));
});

RunTarget(target);