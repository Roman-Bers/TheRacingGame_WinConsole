using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRacingGame_WinConsole
{
    internal static class SharedResources
    {
        static public object Car { get; set; }

        static SharedResources()
        {
            Car = new object();
        }
    }
}
