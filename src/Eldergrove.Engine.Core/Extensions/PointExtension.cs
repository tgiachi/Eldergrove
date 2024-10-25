using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Extensions;

public static class PointExtension
{
    public static Point ToPercengatePoint(this Point originalPoint, float percentage)
    {
        int newX = (int)(originalPoint.X * (percentage / 100));
        int newY = (int)(originalPoint.Y * (percentage / 100));

        return new Point(newX, newY);
    }
}
