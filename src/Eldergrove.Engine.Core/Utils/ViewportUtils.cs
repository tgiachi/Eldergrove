using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Utils;

public static class ViewportUtils
{
    public static Point CalculateViewport(
        Point originalViewport, int originalFontWidth, int originalFontHeight, int newFontWidth, int newFontHeight
    )
    {
        // Calcola il rapporto tra le dimensioni dei font
        float widthRatio = (float)newFontWidth / originalFontWidth;
        float heightRatio = (float)newFontHeight / originalFontHeight;

        // Riduci il numero di celle visibili proporzionalmente al rapporto di dimensioni dei font
        int newViewportWidth = (int)(originalViewport.X / widthRatio);
        int newViewportHeight = (int)(originalViewport.Y / heightRatio);



        return new Point(newViewportWidth, newViewportHeight);
    }
}
