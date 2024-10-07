using Eldergrove.Engine.Core.Interfaces.Services;

namespace Eldergrove.Engine.Core.Services;

public class VersionService : IVersionService
{
    public string GetVersion()
    {
        // Get version from assembly
        var assembly = typeof(VersionService).Assembly;
        var version = assembly.GetName().Version;

        return version.ToString();
    }
}
