using System.Runtime.CompilerServices;
using MyPhotoShop.Models;

namespace MyPhotoShop.Modules;

public class LayerModule
{
    public static unsafe void* CreateLayer(int x, int y)
    {
        byte[] memory = new byte[x * y * 4];
        void* ptr = Unsafe.AsPointer(ref memory);

        Layer layer = new Layer(ptr, x, y);

        return (ptr);
    }
}
