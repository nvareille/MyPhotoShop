using MyPhotoShop.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhotoShop.Models;

public unsafe class Layer
{
    public void* ImagePointer;
    public byte[]? ImageRef;
    public int Width;
    public int Height;
    public int Id;

    public Layer(void* imagePointer, int width, int height)
    {
        ImagePointer = imagePointer;
        Width = width;
        Height = height;
    }

    public Span<byte> GetImage()
    {
        return new(ImagePointer, GetImageSize());
    }

    public int GetImageSize()
    {
        //MyDebugger.Log("Size is " + Width * Height * 4);
        return (Width * Height * 4);
    }

    public void CopyFrom(Layer layer)
    {
        int x = 0;
        Span<byte> src = layer.GetImage();
        Span<byte> dst = GetImage();

        while (x < layer.Width)
        {
            int y = 0;
            
            while (y < layer.Height)
            {
                int d = (y * Width + x) * 4;
                int s = (y * layer.Width + x) * 4;
             
                dst[d] = src[s];
                dst[d + 1] = src[s + 1];
                dst[d + 2] = src[s + 2];
                dst[d + 3] = src[s + 3];

                ++y;
            }

            ++x;
        }
    }
}
