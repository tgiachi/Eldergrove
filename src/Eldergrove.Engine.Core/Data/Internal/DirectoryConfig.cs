using Eldergrove.Engine.Core.Extensions;
using Eldergrove.Engine.Core.Types;

namespace Eldergrove.Engine.Core.Data.Internal;

public class DirectoryConfig
{
    public string RootDirectory { get; }

    public DirectoryConfig(string rootDirectory)
    {
        RootDirectory = rootDirectory;
    }

    public string GetDirectory(DirectoryType directoryType) =>
        Path.Join(RootDirectory, directoryType.ToString().ToSnakeCase());
}
