using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyPhotoShop;

public static unsafe class MemoryManager
{
    [UnmanagedCallersOnly(EntryPoint = "Free")]
    public static void Free(void *ptr)
    {
        NativeMemory.Free(ptr);
        Console.WriteLine("Memory freed");
    }
}
