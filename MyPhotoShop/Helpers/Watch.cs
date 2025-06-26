using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPhotoShop.Logic;

namespace MyPhotoShop.Helpers;

public class Watch
{
    public static void Measure(Action act)
    {
        Stopwatch w = Stopwatch.StartNew();
        act();
        w.Stop();
        MyDebugger.Log("Elapsed: " + w.ElapsedMilliseconds);
    }
}
