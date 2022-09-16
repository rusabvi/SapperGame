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

void SafeInput(ref int number, int min, int max)
{
    string? numberString;

    var converted = false;

    while (!converted)
    {
        numberString = Console.ReadLine();
        if (string.IsNullOrEmpty(numberString))
            numberString = "";

        try
        {
            number = int.Parse(numberString);

            if (number >= min && number <= max)
                converted = true;
            else
                throw new Exception();
        }
        catch
        {
            Console.Write($"Введите число от {min} до {max}: ");
        }
    }
}

var exit = false;
while (!exit)
{
    Console.Clear();

    int rowAmount = new int(), columnAmount = new int(), bombAmount = new int();

    Console.Write("Введите количество рядов: ");
    SafeInput(ref rowAmount, 2, 30);

    Console.Write("Введите количество столбцов: ");
    SafeInput(ref columnAmount, 2, 30);

    Console.Write("Введите количество бомб: ");
    SafeInput(ref bombAmount, 1, rowAmount * columnAmount - 1);

    Game game;

    Console.WriteLine("Игра началась!");
    Thread.Sleep(2000);
    Console.Clear();

    int row = new int(), column = new int();
    Console.WriteLine("Выберите ячейку");
    Console.Write("Ряд: ");
    SafeInput(ref row, 1, rowAmount);
    Console.Write("Столбец: ");
    SafeInput(ref column, 1, columnAmount);
    game = new Game(rowAmount, columnAmount, bombAmount, row - 1, column - 1);
    Console.Clear();

    while (!(game.IsOver() || game.IsWon()))
    {
        DrawField(game.GetCells());
        Console.WriteLine("Выберите ячейку");
        Console.Write("Ряд: ");
        SafeInput(ref row, 1, rowAmount);
        Console.Write("Столбец: ");
        SafeInput(ref column, 1, columnAmount);
        game.TryToOpenCell(row - 1, column - 1);
        Console.Clear();
    }

    if (game.IsOver())
        Console.WriteLine("Вы проиграли!");
    else
        Console.WriteLine("Вы выиграли!");

    Console.WriteLine("Хотите ещё сыграть?\ny) Да\nn) Нет");
    var answer = Console.ReadLine();
    if (string.IsNullOrEmpty(answer))
        answer = "";

    while (!(answer.Equals("y") || answer.Equals("Y") || answer.Equals("n") || answer.Equals("N")))
    {
        Console.WriteLine("Введите \"y\" или \"n\"");
        answer = Console.ReadLine();
        if (string.IsNullOrEmpty(answer))
            answer = "";
    }

    if (answer.Equals("n") || answer.Equals("N"))
        exit = true;
}