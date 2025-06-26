using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyPhotoShop.Models;

public class LayerManager
{
    public List<Layer> Layers = new ();

    public LayerManager()
    {
        Console.WriteLine("CTR");
    }

    public unsafe Layer CreateLayer(void *ptr, int x, int y)
    {
        int id = 1;
        Layer layer = new Layer(ptr, x, y);

        if (Layers.Any())
            id = Layers.Max(i => i.Id);

        layer.Id = id;
        Layers.Add(layer);

        return (layer);
    }

    public unsafe Layer CreateLayer(byte[] ptr, int x, int y)
    {
        Layer l = CreateLayer(Unsafe.AsPointer(ref ptr[0]), x, y);

        l.SSav = ptr;
        return (l);
    }

    public unsafe Layer CreateLayer(int x, int y)
    {
        return (CreateLayer(new byte[4 * x * y], x, y));
    }
}
