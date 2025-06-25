using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhotoShop.Models;

public unsafe class MyPhotoShopImage
{
    public void* ImagePointer;
    public int Width;
    public int Height;

    public MyPhotoShopImage(void* imagePointer, int width, int height)
    {
        ImagePointer = imagePointer;
        Width = width;
        Height = height;
    }

    public Span<byte> GetImage()
    {
        return new(ImagePointer, Width * Height * 4);
    }
}
