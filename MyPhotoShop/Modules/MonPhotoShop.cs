using System.Runtime.InteropServices;
using MyPhotoShop.Logic;

namespace MyPhotoShop.Modules
{
    public unsafe class MonPhotoShop
    {
        [UnmanagedCallersOnly(EntryPoint = "Grayscale")]
        public static void Grayscale()
        {
            if (MemoryManager.Image == null)
            {
                MyDebugger.Log("Image is empty");
                return;
            }

            Span<byte> bytes = MemoryManager.Image.GetImage();

            int count = 0;

            while (count < MemoryManager.Image.GetImageSize())
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

        }
    }
}
