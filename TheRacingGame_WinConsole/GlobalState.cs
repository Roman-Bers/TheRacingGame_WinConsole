using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRacingGame_WinConsole
{
    internal static class GlobalState
    {
        static public int Health { get; private set; }
        static public int Points { get; private set; }
        static public int Speed { get; private set; }
        static public int ImmortalModePoints { get; private set; }
        static public bool IsGameOver { get; private set; }

        static GlobalState()
        {
            Health = 5;
            Speed = 500;
        }

        static public void LeaveHealth()
        {
            Health--;
        }

        static public void AddPoint()
        {
            Points++;
        }

        static public void AddImmortalModePoints()
        {
            ImmortalModePoints += 10;
        }

        static public void RemoveImmortalModePoints()
        {
            if (ImmortalModePoints > 0)
                ImmortalModePoints--;
        }

        static public void SpeedUp()
        {
            if (Speed > 0)
                Speed--;
        }

        static public void SetGameOver()
        {
            IsGameOver = true;
        }
    }
}
