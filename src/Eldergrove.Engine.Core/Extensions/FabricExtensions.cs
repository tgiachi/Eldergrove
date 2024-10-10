using Eldergrove.Engine.Core.Types;

namespace Eldergrove.Engine.Core.Extensions;

public static class FabricExtensions
{
    public static string[] RandomRotateFabric(this string[] fabric) =>
        fabric.RotateFabric((RotationAngleType)Random.Shared.Next(0, 3));

    public static string[] RotateFabric(this string[] fabric, RotationAngleType angle)
    {
        char[][] matrix = fabric.Select(x => x.ToCharArray()).ToArray();

        char[][] rotatedMatrix = angle switch
        {
            RotationAngleType.Rotate90  => Rotate90(matrix),
            RotationAngleType.Rotate180 => Rotate180(matrix),
            RotationAngleType.Rotate270 => Rotate270(matrix),
            _                           => throw new ArgumentException("Invalid rotation angle.")
        };

        return rotatedMatrix.Select(row => new string(row)).ToArray();
    }

    private static char[][] Rotate90(char[][] matrix)
    {
        int width = matrix.Length;
        int height = matrix[0].Length;
        char[][] result = new char[height][];

        for (int i = 0; i < height; i++)
        {
            result[i] = new char[width];
            for (int j = 0; j < width; j++)
            {
                result[i][j] = matrix[width - j - 1][i];
            }
        }

        return result;
    }

    private static char[][] Rotate180(char[][] matrix)
    {
        int width = matrix.Length;
        int height = matrix[0].Length;
        char[][] result = new char[width][];

        for (int i = 0; i < width; i++)
        {
            result[i] = new char[height];
            for (int j = 0; j < height; j++)
            {
                result[i][j] = matrix[width - i - 1][height - j - 1];
            }
        }

        return result;
    }

    private static char[][] Rotate270(char[][] matrix)
    {
        int width = matrix.Length;
        int height = matrix[0].Length;
        char[][] result = new char[height][];

        for (int i = 0; i < height; i++)
        {
            result[i] = new char[width];
            for (int j = 0; j < width; j++)
            {
                result[i][j] = matrix[j][height - i - 1];
            }
        }

        return result;
    }
}
