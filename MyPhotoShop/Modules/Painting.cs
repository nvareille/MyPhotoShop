using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MyPhotoShop.Logic;
using MyPhotoShop.Models;

namespace MyPhotoShop.Modules;

public static class Painting
{
    private static Brush Brush = new Brush(5, Color.Red);

    [UnmanagedCallersOnly(EntryPoint = "ApplyPaint")]
    public static void ApplyPaint(int x, int y)
    {
        MyDebugger.Log("Painting on " + x + " " + y);
        Brush.Apply(MemoryManager.Image!, x, y);
    }
}
