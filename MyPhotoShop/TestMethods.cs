using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyPhotoShop;

public static unsafe class TestMethods
{
    [UnmanagedCallersOnly(EntryPoint = "SayHello")]
    public static void SayHello()
    {
        Console.WriteLine("Hello World");
    }

    [UnmanagedCallersOnly(EntryPoint = "GetTheNumber")]
    public static int GetTheNumber()
    {
        return (42);
    }
}
