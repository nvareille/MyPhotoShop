using System.Runtime.InteropServices;
using MyPhotoShop.Helpers;
using MyPhotoShop.Logic;
using MyPhotoShop.Models;

namespace MyPhotoShop.Modules
{
    public unsafe class MonPhotoShop
    {
        [UnmanagedCallersOnly(EntryPoint = "Grayscale")]
        public static void Grayscale()
        {
            Watch.Measure(() =>
            {

            
                if (MemoryManager.LayerManager.Layers.Count == 0)
                {
                    MyDebugger.Log("Image is empty");
                    return;
                }

                Layer layer = MemoryManager.LayerManager.Layers.First();
                Span<byte> bytes = layer.GetImage();

                int count = 0;
                int size = layer.GetImageSize();

                while (count < size)
                {
                    byte r = bytes[count];
                    byte g = bytes[count + 1];
                    byte b = bytes[count + 2];

                    byte v = (byte)((r + g + b) / 3);

                    bytes[count] = v;
                    bytes[count + 1] = v;
                    bytes[count + 2] = v;
                    count += 4;
                }

            });
        }
        
    }
}
