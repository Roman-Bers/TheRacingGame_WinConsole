using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRacingGame_WinConsole.Enums;

namespace TheRacingGame_WinConsole
{
    internal static class Car
    {
        static private CarElement[,] _carBuffer;
        static private int _leftOffset;

        static Car()
        {
            _carBuffer = new CarElement[Constants.ROAD_FRAGMENT_ROW_LENGTH, Constants.ROAD_COL_LENGTH];
            _init();
        }

        static public CarElement[,] GetCar()
        {
            CarElement[,] copyCarBuffer = new CarElement[_carBuffer.GetLength(0), _carBuffer.GetLength(1)];

            for (int row = 0; row < _carBuffer.GetLength(0); row++)
            {
                for (int col = 0; col < _carBuffer.GetLength(1); col++)
                {
                    copyCarBuffer[row, col] = _carBuffer[row, col];
                }
            }

            return copyCarBuffer;
        }

        static public void DriveRight()
        {
            if (_leftOffset >= 18)
                return;

            _leftOffset++;
            _drawInPosition(_leftOffset);
        }

        static public void DriveLeft()
        {
            if (_leftOffset <= 0)
                return;

            _leftOffset--;
            _drawInPosition(_leftOffset);
        }

        static private void _init()
        {
            _leftOffset = 9;
            _drawInPosition(_leftOffset);
        }

        static private void _drawInPosition(int leftOffset)
        {
            _clear();
            _carBuffer[2, 0 + leftOffset] = CarElement.Car;
            _carBuffer[2, 1 + leftOffset] = CarElement.Car;
            _carBuffer[2, 2 + leftOffset] = CarElement.Car;
            _carBuffer[2, 3 + leftOffset] = CarElement.Car;
            _carBuffer[2, 4 + leftOffset] = CarElement.Car;
            _carBuffer[2, 5 + leftOffset] = CarElement.Car;

            _carBuffer[1, 2 + leftOffset] = CarElement.Car;
            _carBuffer[1, 3 + leftOffset] = CarElement.Car;

            _carBuffer[4, 0 + leftOffset] = CarElement.Car;
            _carBuffer[4, 1 + leftOffset] = CarElement.Car;
            _carBuffer[4, 2 + leftOffset] = CarElement.Car;
            _carBuffer[4, 3 + leftOffset] = CarElement.Car;
            _carBuffer[4, 4 + leftOffset] = CarElement.Car;
            _carBuffer[4, 5 + leftOffset] = CarElement.Car;

            _carBuffer[3, 2 + leftOffset] = CarElement.Car;
            _carBuffer[3, 3 + leftOffset] = CarElement.Car;
        }

        static private void _clear()
        {
            for (int row = 0; row < _carBuffer.GetLength(0); row++)
            {
                for (int col = 0; col < _carBuffer.GetLength(1); col++)
                {
                    _carBuffer[row, col] = CarElement.Empty;
                }
            }
        }
    }
}
