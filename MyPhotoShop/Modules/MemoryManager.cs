using MyPhotoShop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MyPhotoShop.Logic;

namespace MyPhotoShop.Modules;

public static unsafe class MemoryManager
{
    public static LayerManager LayerManager = new ();

    [UnmanagedCallersOnly(EntryPoint = "SetDebug")]
    public static void LoadImage(bool enabled)
    {
        MyDebugger.Enabled = enabled;
        MyDebugger.Log("Debug mode Enabled");
    }

    [UnmanagedCallersOnly(EntryPoint = "LoadImage")]
    public static unsafe void LoadImage(void *ptr, int sizeX, int sizeY)
    {
        Console.WriteLine("1 " + LayerManager.Layers.Count);
        Layer l = LayerManager.CreateLayer(sizeX, sizeY);
     
        Span<byte> s = l.GetImage();
        Span<byte> b = new(ptr, sizeX * sizeY * 4);

        b.CopyTo(s);

        MyDebugger.Log("Image loaded " + sizeX + " " + sizeY);
        Console.WriteLine(l.GetImage()[0]);
        Console.WriteLine("2 " + LayerManager.Layers.Count);
    }

    [UnmanagedCallersOnly(EntryPoint = "Free")]
    public static void Free(void *ptr)
    {
        NativeMemory.Free(ptr);
        Console.WriteLine("Memory freed");
    }
}
