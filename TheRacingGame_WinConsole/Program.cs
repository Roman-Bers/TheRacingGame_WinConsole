using TheRacingGame_WinConsole;
using TheRacingGame_WinConsole.Enums;
using System.Text;


Console.OutputEncoding = Encoding.Unicode;
Console.CursorVisible = false;


Task.Run(() =>
{
    while (true)
    {
        ConsoleKeyInfo ki = Console.ReadKey();

        if (ki.Key == ConsoleKey.RightArrow)
        {
            lock (SharedResources.Car)
            {
                Car.DriveRight();
            }
        }
        else if (ki.Key == ConsoleKey.LeftArrow)
        {
            lock (SharedResources.Car)
            {
                Car.DriveLeft();
            }
        }
    }
});

RoadElement[,] oldRoad = null;
CarElement[,] oldCar = null;
while (true)
{
    //Console.Clear();

    if (oldRoad is not null)
        _printRoad(oldRoad, ConsoleColor.Black, ConsoleColor.Black, Constants.SCREEN_TOP_POSITION, Constants.SCREEN_LEFT_POSITION);

    RoadElement[,] road = Road.GetNextRoad();
    _printRoad(road, ConsoleColor.Blue, ConsoleColor.Yellow, Constants.SCREEN_TOP_POSITION, Constants.SCREEN_LEFT_POSITION);

    oldRoad = road;


    Monitor.Enter(SharedResources.Car);
    CarElement[,] car = Car.GetCar();
    Monitor.Exit(SharedResources.Car);

    int carTopOffset = road.GetLength(0) - car.GetLength(0);

    if (oldCar is not null)
        _printCar(oldCar, ConsoleColor.Black, carTopOffset, Constants.SCREEN_TOP_POSITION, Constants.SCREEN_LEFT_POSITION);

    ConsoleColor carColor = GlobalState.ImmortalModePoints == 0 ? ConsoleColor.Green : ConsoleColor.DarkGray;
    _printCar(car, carColor, carTopOffset, Constants.SCREEN_TOP_POSITION, Constants.SCREEN_LEFT_POSITION);

    oldCar = car;

    bool isCollision = _isCollision(road, car);
    if(GlobalState.ImmortalModePoints == 0 && isCollision)
    {
        GlobalState.LeaveHealth();
        GlobalState.AddImmortalModePoints();
    }

    if(GlobalState.Health == 0)
    {
        GlobalState.SetGameOver();
        break;
    }

    GlobalState.RemoveImmortalModePoints();
    GlobalState.SpeedUp();

    Thread.Sleep(GlobalState.Speed);
}

Console.ReadKey();

static bool _isCollision(RoadElement[,] road, CarElement[,] car)
{
    for (int row = 0; row < car.GetLength(0); row++)
    {
        for (int col = 0; col < car.GetLength(1); col++)
        {
            if (car[row, col] == CarElement.Car)
            {
                int carTopOffset = road.GetLength(0) - car.GetLength(0);
                if (road[row + carTopOffset, col] == RoadElement.Barier)
                {
                    return true;
                }

                if (road[row + carTopOffset, col] == RoadElement.Bonus)
                {
                    GlobalState.AddPoint();
                    Road.HideBonus(row + carTopOffset, col);
                }
            }
        }
    }

    return false;
}

static void _printRoad(RoadElement[,] road, ConsoleColor colorBarier, ConsoleColor colorBonus, int positionTop, int positionLeft)
{
    Console.SetCursorPosition(0 + positionLeft, 0 + positionTop);

    for (int row = 0; row < road.GetLength(0); row++)
    {
        for (int col = 0; col < road.GetLength(1); col++)
        {
            Console.SetCursorPosition(col + positionLeft, row + positionTop);

            if (road[row, col] == RoadElement.Barier)
            {
                Console.ForegroundColor = colorBarier;
                Console.Write("#");
            }
            else if (road[row, col] == RoadElement.Bonus)
            {
                Console.ForegroundColor = colorBonus;
                Console.Write("♥");
            }
        }
    }
}


static void _printCar(CarElement[,] car, ConsoleColor color, int topOffset, int positionTop, int positionLeft)
{
    Console.ForegroundColor = color;

    Console.SetCursorPosition(0 + positionLeft, topOffset + positionTop);

    for (int row = 0; row < car.GetLength(0); row++)
    {
        for (int col = 0; col < car.GetLength(1); col++)
        {
            Console.SetCursorPosition(col + positionLeft, row + topOffset + positionTop);

            if (car[row, col] == CarElement.Car)
            {
                Console.Write("♦");
            }
        }
    }
}
