using Sapper;

void DrawField(List<List<Cell>> cells)
{
    for (int i = 0; i < cells.Count; i++)
    {
        for (int j = 0; j < cells[i].Count; j++)
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

void SafePrint(ref int number, int min, int max)
{
    string numberString = Console.ReadLine();
    while (!int.TryParse(numberString, out number) && number >= min && number <= max)
    {
        Console.Write($"Введите число от {min} до {max}: ");
        numberString = Console.ReadLine();
    }
}

int rowAmount = new int(), columnAmount = new int(), bombAmount = new int();

Console.Write("Введите количество рядов: ");
SafePrint(ref rowAmount, 2, 30);

Console.Write("Введите количество столбцов: ");
SafePrint(ref columnAmount, 2, 30);

Console.Write("Введите количество бомб: ");
SafePrint(ref bombAmount, 1, rowAmount * columnAmount - 1);

var game = new Game(rowAmount, columnAmount, bombAmount);

Console.WriteLine("Игра началась!");
Thread.Sleep(2000);
Console.Clear();

while (!game.IsOver() || game.GetOpenedCellAmount() != rowAmount * columnAmount)
{
    DrawField(game.GetCells());
    int row = new int(), column = new int();
    Console.WriteLine("Выберите ячейку");
    Console.Write("Ряд: ");
    SafePrint(ref row, 1, rowAmount);
    Console.Write("Столбец: ");
    SafePrint(ref column, 1, columnAmount);
    game.CheckCellIfItIsBombed(row - 1, column - 1);
    Console.Clear();
}

if (game.IsOver())
    Console.WriteLine("Вы проиграли!");
else
    Console.WriteLine("Вы выиграли!");