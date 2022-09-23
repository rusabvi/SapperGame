namespace Sapper
{
    public class Game
    {
        private List<List<Cell>> _cells;
        private bool _over;
        private bool _won;
        private int _openedCellAmount;
        private int _bombAmount;

        public Game(int rowAmount, int columnAmount, int bombAmount, int firstRow, int firstColumn)
        {
            if (rowAmount < 1)
                throw new Exception("Row amount should be more 0");

            if (bombAmount < 1)
                throw new Exception("Bomb amount should be more 0");

            if (firstRow < 0 || firstRow > rowAmount - 1)
                throw new Exception("First row should be more -1 and less row amount");

            if (firstColumn < 0 || firstColumn > columnAmount - 1)
                throw new Exception("First column should be more -1 and less column amount");

            _openedCellAmount = 0;
            _bombAmount = bombAmount;
            _over = false;
            _won = false;
            _cells = new List<List<Cell>>();

            CreateVoidCells(rowAmount, columnAmount);
            OpenFirstCellAndBombCells(firstRow, firstColumn, bombAmount);
            CheckCells();
        }

        private void CreateVoidCells(int rowAmount, int columnAmount)
        {
            for (int i = 0; i < rowAmount; i++)
            {
                var cellList = new List<Cell>();

                for (int j = 0; j < columnAmount; j++)
                    cellList.Add(new Cell(false));

                _cells.Add(cellList);
            }
        }

        private void OpenFirstCellAndBombCells(int firstRow, int firstColumn, int bombAmount)
        {
            var firstCell = _cells[firstRow][firstColumn];
            firstCell.Open();

            for (int b = 0; b < bombAmount; b++)
            {
                var random = new Random();
                int row = random.Next(0, _cells.Count - 1),
                    column = random.Next(0, _cells[0].Count - 1);
                var cell = _cells[row][column];
                if (!cell.GetValue().Equals(CellValue.Bomb) && !cell.Equals(firstCell))
                    cell.Bomb();

                else
                    b--;
            }
        }

        private void CheckCells()
        {
            for (int i = 0; i < _cells.Count; i++)
                for (int j = 0; j < _cells[i].Count; j++)
                {
                    var cellToCheck = _cells[i][j];

                    if (!cellToCheck.IsBombed())
                    {
                        for (int k = -1; k < 2; k++)
                            for (int l = -1; l < 2; l++)
                                if (!(k == 0 && l == 0))
                                {
                                    try
                                    {
                                        var cellNear = _cells[i + k][j + l];
                                        if (cellNear.IsBombed())
                                            cellToCheck.AddOneBombAround();
                                    }
                                    catch { ; }
                                }
                    }
                }
        }


        public void TryToOpenCell(int row, int column)
        {
            if (row < 0 || row > _cells.Count - 1)
                throw new Exception("Row should be more -1 and less row amount");

            if (column < 0 || column > _cells[0].Count - 1)
                throw new Exception("Column should be more -1 and less column amount");

            var cellToTry = _cells[row][column];

            if (cellToTry.GetValue().Equals(CellValue.Unknown))
            {
                _openedCellAmount++;
                cellToTry.Open();
               _over = cellToTry.GetValue().Equals(CellValue.Bomb);
                _won = _openedCellAmount >= _cells.Count * _cells[0].Count - _bombAmount - 1;
            }
        }

        public List<List<Cell>> GetCells() => _cells;

        public bool IsOver() => _over;
        public bool IsWon() => _won;
    }
}