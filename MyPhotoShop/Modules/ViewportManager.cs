using MyPhotoShop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MyPhotoShop.Helpers;
using MyPhotoShop.Logic;

namespace MyPhotoShop.Modules;

public static class ViewportManager
{
    public static Layer? Viewport;

    [UnmanagedCallersOnly(EntryPoint = "GetViewport")]
    public static unsafe IntPtr GetViewport()
    {
        
        ComputeViewport(); 
        ApplyViewport();
    
        

        return ((IntPtr)Viewport!.ImagePointer);
    }

    private static unsafe void ApplyViewport()
    {
        foreach (Layer layer in MemoryManager.LayerManager.Layers)
        {
            Viewport!.CopyFrom(layer);
        }
    }

    [UnmanagedCallersOnly(EntryPoint = "GetViewportSizeX")]
    public static int GetViewportSizeX()
    {
        return (Viewport!.Width);
    }

    [UnmanagedCallersOnly(EntryPoint = "GetViewportSizeY")]
    public static int GetViewportSizeY()
    {
        return (Viewport!.Height);
    }

    private static unsafe void ComputeViewport()
    {
        if (MemoryManager.LayerManager.Layers.Count == 0)
        {
            GenViewport(1, 1);
            return;
        }

        int x = MemoryManager.LayerManager.Layers.Max(i => i.Width);
        int y = MemoryManager.LayerManager.Layers.Max(i => i.Height);

        if (Viewport == null || Viewport.Width != x || Viewport.Height != y)
        {
            GenViewport(x, y);
        }
    }

    private static unsafe void GenViewport(int x, int y)
    {
        byte[] bytes = new byte[4 * x * y];
        void *ptr = Unsafe.AsPointer(ref bytes[0]);

        Viewport = new Layer(ptr, x, y);
        Viewport.ImageRef = bytes;
    }
}
