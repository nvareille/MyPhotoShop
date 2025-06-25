using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhotoShop.Logic;

public class MyDebugger
{
    public static bool Enabled;

    public static void Log(string str)
    {
        if (Enabled)
            Console.WriteLine(str);
    }
}
