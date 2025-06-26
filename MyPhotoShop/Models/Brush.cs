using System.Drawing;

namespace MyPhotoShop.Models;

public class Brush
{
    public int Size;
    public Color Color;

    public Brush(int size, Color color)
    {
        Size = size;
        Color = color;
    }

    public void Apply(Layer image, int x, int y)
    {
        Span<byte> span = image.GetImage();

        int tempX = x - Size;

        while (tempX < x + Size)
        {
            int tempY = y - Size;

            while (tempY < y + Size)
            {
                ApplyOnPoint(span, image.Width, tempX, tempY);
                ++tempY;
            }

            ++tempX;
        }
    }

    private void ApplyOnPoint(Span<byte> span, int imgWidth, int x, int y)
    {
        int idx = (y * imgWidth + x) * 4;

        span[idx] = Color.R;
        span[idx + 1] = Color.G;
        span[idx + 2] = Color.B;
        span[idx + 3] = Color.A;
    }
}
