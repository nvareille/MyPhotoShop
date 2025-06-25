using System.Runtime.InteropServices;

namespace MyPhotoShop
{
    public unsafe class MonPhotoShop
    {
               

        [UnmanagedCallersOnly(EntryPoint = "Grayscale")]
        public static void Grayscale(void *ptr, int size)
        {
            Span<byte> bytes = new (ptr, size);

            int count = 0;

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

        }
    }
}
