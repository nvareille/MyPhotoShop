using MyPhotoShop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MyPhotoShop.Logic;

namespace MyPhotoShop.Modules;

public static unsafe class MemoryManager
{
    public static MyPhotoShopImage? Image;

    [UnmanagedCallersOnly(EntryPoint = "SetDebug")]
    public static void LoadImage(bool enabled)
    {
        MyDebugger.Enabled = enabled;
        MyDebugger.Log("Debug mode Enabled");
    }

    [UnmanagedCallersOnly(EntryPoint = "LoadImage")]
    public static void LoadImage(void *ptr, int sizeX, int sizeY)
    {
        Image = new MyPhotoShopImage(ptr, sizeX, sizeY);
        MyDebugger.Log("Image loaded " + sizeX + " " + sizeY);
    }

    [UnmanagedCallersOnly(EntryPoint = "Free")]
    public static void Free(void *ptr)
    {
        NativeMemory.Free(ptr);
        Console.WriteLine("Memory freed");
    }
}
