namespace Eldergrove.Engine.Core.Utils;

public static class DirectoriesUtils
{
    public static IEnumerable<string> ScanDirectory(string directory, string fileType = "*.*") =>
        Directory.GetFiles(directory, fileType, SearchOption.AllDirectories);
}
