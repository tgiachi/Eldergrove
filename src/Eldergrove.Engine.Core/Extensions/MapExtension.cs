using GoRogue.GameFramework;
using GoRogue.Random;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Extensions;

public static class MapExtension
{
    private static bool IsAreaFree(Point topLeft, int width, int height, Map map)
    {
        for (int x = topLeft.X; x < topLeft.X + width; x++)
        {
            for (int y = topLeft.Y; y < topLeft.Y + height; y++)
            {
                if (!map.WalkabilityView[x, y])
                {
                    return false;
                }
            }
        }

        return true;
    }

    public static List<Point>? FindFreeArea(this Map map, int width, int height, int maxAttempts = 200)
    {
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            int randomX = GlobalRandom.DefaultRNG.NextInt(0, map.Width - width);
            int randomY = GlobalRandom.DefaultRNG.NextInt(0, map.Height - height);

            Point randomStart = new Point(randomX, randomY);

            if (IsAreaFree(randomStart, width, height, map))
            {
                List<Point> areaPoints = new List<Point>();
                for (int x = randomStart.X; x < randomStart.X + width; x++)
                {
                    for (int y = randomStart.Y; y < randomStart.Y + height; y++)
                    {
                        areaPoints.Add(new Point(x, y));
                    }
                }

                return areaPoints;
            }
        }


        return null;
    }


}
