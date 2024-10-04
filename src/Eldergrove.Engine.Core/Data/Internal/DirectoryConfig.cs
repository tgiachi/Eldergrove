using Eldergrove.Engine.Core.Extensions;
using Eldergrove.Engine.Core.Types;

namespace Eldergrove.Engine.Core.Data.Internal;

public class DirectoryConfig
{
    public string RootDirectory { get; }

    public DirectoryConfig(string rootDirectory)
    {
        RootDirectory = rootDirectory;

        CheckDirectories();
    }


    private void CheckDirectories()
    {
        if (!Directory.Exists(RootDirectory))
        {
            Directory.CreateDirectory(RootDirectory);
        }

        foreach (var directoryType in Enum.GetValues<DirectoryType>())
        {
            var directory = GetDirectory(directoryType);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }

    public string GetDirectory(DirectoryType directoryType) =>
        Path.Join(RootDirectory, directoryType.ToString().ToSnakeCase());
}
