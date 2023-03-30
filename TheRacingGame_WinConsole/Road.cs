using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRacingGame_WinConsole.Enums;
using TheRacingGame_WinConsole.Models;

namespace TheRacingGame_WinConsole
{
    internal static class Road
    {
        static private RoadElement[,] _roadBuffer;
        static private int _startWindowIndex;
        static private FragmentElementType _nextFragmentType;

        static Road()
        {
            _startWindowIndex = 6;
            _nextFragmentType = FragmentElementType.Bonus;

            _roadBuffer = new RoadElement[Constants.ROAD_ROW_LENGTH, Constants.ROAD_COL_LENGTH];

            _insertFragmentToRoad(_generateRandomFragment(FragmentElementType.Bonus), 0);
            _insertFragmentToRoad(_generateRandomFragment(FragmentElementType.Barier), 6);
            _insertFragmentToRoad(_generateRandomFragment(FragmentElementType.Bonus), 12);
            _insertFragmentToRoad(_generateRandomFragment(FragmentElementType.Barier), 18);
            _insertFragmentToRoad(_generateRandomFragment(FragmentElementType.Bonus), 24);
        }

        static public RoadElement[,] GetNextRoad()
        {
           if(_startWindowIndex == 0)
            {
                _startWindowIndex = 6;

                _nextFragmentType = _nextFragmentType == FragmentElementType.Bonus ? FragmentElementType.Barier : FragmentElementType.Bonus;

                _moveRoadBufferDownOnOneFragment();
                _insertFragmentToRoad(_generateRandomFragment(_nextFragmentType), 0);
            }
            
            RoadElement[,] roadWindow = _getRoadWindow();

            _startWindowIndex--;

            return roadWindow;
        }

        static public void HideBonus(int row, int col)
        {
            //TODO: ПРОВЕРИТЬ ПОЧИНИТЬ
            _roadBuffer[row + _startWindowIndex, col] = RoadElement.Empty;
        }

        static private RoadElement[,] _getRoadWindow()
        {
            RoadElement[,] roadWindow = new RoadElement[Constants.ROAD_WINDOW_ROW_LENGTH, Constants.ROAD_COL_LENGTH];

            for (int row = 0; row < roadWindow.GetLength(0); row++)
            {
                for (int col = 0; col < roadWindow.GetLength(1); col++)
                {
                    roadWindow[row, col] = _roadBuffer[row + _startWindowIndex, col];
                }
            }

            return roadWindow;
        }

        static private void _moveRoadBufferDownOnOneFragment()
        {
            for (int row = _roadBuffer.GetLength(0) - 1; row >= Constants.ROAD_FRAGMENT_ROW_LENGTH; row--)
            {
                for (int col = 0; col < _roadBuffer.GetLength(1); col++)
                {
                    _roadBuffer[row, col] = _roadBuffer[row - Constants.ROAD_FRAGMENT_ROW_LENGTH, col];
                }
            }
        }

        static private void _insertFragmentToRoad(RoadElement[,] _innerFragment, int rowIndex)
        {
            for (int row = 0; row < _innerFragment.GetLength(0); row++)
            {
                for (int col = 0; col < _innerFragment.GetLength(1); col++)
                {
                    _roadBuffer[row + rowIndex, col] = _innerFragment[row, col];
                }
            }
        }

        static private RoadElement[,] _generateRandomFragment(FragmentElementType elementType)
        {
            RoadElement[,] fragment = new RoadElement[Constants.ROAD_FRAGMENT_ROW_LENGTH, Constants.ROAD_COL_LENGTH];
           
            switch (elementType)
            {
                case FragmentElementType.Bonus:
                    {
                        int leftOffset = Random.Shared.Next(2, 21);
                        _addRectangle(fragment, new ElementPoint(2, 0 + leftOffset), new ElementPoint(3, 1 + leftOffset), RoadElement.Bonus);
                    }
                    break;
                case FragmentElementType.Barier:
                    {
                        BarierType barierType = (BarierType)Random.Shared.Next(0, 6);

                        switch (barierType)
                        {
                            case BarierType.Center:
                                _addRectangle(fragment, new ElementPoint(1, 8), new ElementPoint(4, 15), RoadElement.Barier);
                                break;
                            case BarierType.ShortRight:
                                _addRectangle(fragment, new ElementPoint(1, 0), new ElementPoint(4, 7), RoadElement.Barier);
                                break;
                            case BarierType.ShortLeft:
                                _addRectangle(fragment, new ElementPoint(1, 16), new ElementPoint(4, 23), RoadElement.Barier);
                                break;
                            case BarierType.Double:
                                _addRectangle(fragment, new ElementPoint(1, 0), new ElementPoint(4, 7), RoadElement.Barier);
                                _addRectangle(fragment, new ElementPoint(1, 16), new ElementPoint(4, 23), RoadElement.Barier);
                                break;
                            case BarierType.LongRight:
                                _addRectangle(fragment, new ElementPoint(1, 0), new ElementPoint(4, 15), RoadElement.Barier);
                                break;
                            case BarierType.LongLeft:
                                _addRectangle(fragment, new ElementPoint(1, 8), new ElementPoint(4, 23), RoadElement.Barier);
                                break;
                        }
                    }
                    break;
            }

            return fragment;
        }

        static private void _addRectangle(RoadElement[,] fragment, ElementPoint leftTop, ElementPoint rightBottom, RoadElement element)
        {
            for (int row = 0; row < fragment.GetLength(0); row++)
            {
                for (int col = 0; col < fragment.GetLength(1); col++)
                {
                    if(row >= leftTop.Row && row <= rightBottom.Row && col >= leftTop.Col && col<=rightBottom.Col)
                    {
                        fragment[row, col] = element;
                    }
                }
            }
        }

    }

}
