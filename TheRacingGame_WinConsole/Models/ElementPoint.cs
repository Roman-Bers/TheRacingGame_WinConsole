using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRacingGame_WinConsole.Models
{
    internal struct ElementPoint
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public ElementPoint(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
}
