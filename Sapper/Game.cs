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
            _openedCellAmount = 0;
            _bombAmount = bombAmount;
            _over = false;
            _won = false;
            _cells = new List<List<Cell>>();

            for (int i = 0; i < rowAmount; i++)
            {
                var cellList = new List<Cell>();

                for (int j = 0; j < columnAmount; j++)
                    cellList.Add(new Cell(false));

                _cells.Add(cellList);
            }

            var firstCell = _cells[firstRow][firstColumn];
            firstCell.Open();

            for (int b = 0; b < bombAmount; b++)
            {
                var random = new Random();
                int row = random.Next(0, rowAmount - 1), column = random.Next(0, columnAmount - 1);
                var cell = _cells[row][column];

                if (!cell.IsBombed() && !cell.Equals(firstCell))
                    cell.Bomb();
                else
                    b--;
            }

            CheckCells();
        }

        public void TryToOpenCell(int row, int column)
        {
            var cellToTry = _cells[row][column];
            if (cellToTry.IsBombed())
                _over = true;
            if (!cellToTry.IsOpened())
                _openedCellAmount++;
            cellToTry.Open();
            _won = _openedCellAmount >= _cells.Count * _cells[0].Count - 1 - _bombAmount;
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

        public int GetOpenedCellAmount() => _openedCellAmount;
        public bool IsOver() => _over;
        public bool IsWon() => _won;

        public List<List<Cell>> GetCells()
        {
            var cells = new List<List<Cell>>();
            for (int i = 0; i < _cells.Count; i++)
            {
                var cellRow = new List<Cell>();

                for (int j = 0; j < _cells[i].Count; j++)
                    cellRow.Add(new Cell(_cells[i][j].IsBombed()));
            }

            return cells;
        }
    }
}