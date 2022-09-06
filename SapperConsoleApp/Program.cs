using Sapper;

void DrawField(List<List<Cell>> cells)
{
    for (byte i = 0; i < cells.Count; i++)
    {
        for (byte j = 0; j < cells[i].Count; j++)
        {
            var cell = cells[i][j];
            if (cell.IsOpened())
                Console.Write(cell.GetBombsAroundAmount());
            else
                Console.Write("*");
        }
        Console.Write("\n");
    }
}

void SafePrint(ref byte number, byte min, byte max)
{
    string numberString = Console.ReadLine();
    while (!byte.TryParse(numberString, out number))
    {
        Console.Write($"Введите число от {min} до {max}: ");
        numberString = Console.ReadLine();
    }
}

Console.Write("Введите количество рядов: ");
byte rowAmount = new byte();
SafePrint(ref rowAmount, 2, 30);

Console.Write("Введите количество столбцов: ");
byte columnAmount = new byte();
SafePrint(ref columnAmount, 2, 30);

var game = new Game(rowAmount, columnAmount);
Console.WriteLine("Игра началась!");
Thread.Sleep(2000);
Console.Clear();

while (!game.IsOver())
{
    DrawField(game.GetCells());
    byte row, column;
    Console.WriteLine("Выберите ячейку");
    Console.Write("Ряд: ");
    row = Convert.ToByte(Console.ReadLine());
    Console.Write("Столбец: ");
    column = Convert.ToByte(Console.ReadLine());
    game.CheckCellIfItIsBomb(row, column);
    Console.Clear();
}

if (game.IsOver())
    Console.WriteLine("Вы проиграли!");
else
    Console.WriteLine("Вы выиграли!");