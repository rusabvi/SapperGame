namespace Sapper
{
    public class Game
    {
        private List<List<Cell>> _cells;
        private bool _over;
        private bool _won;
        private int _openedCellAmount;
        private int _bombAmount;

        public Game(int rowAmount, int columnAmount, int bombAmount)
        {
            _openedCellAmount = 0;
            _bombAmount = bombAmount;
            _over = false;
            _won = false;
            _cells = new List<List<Cell>>();
            for (int i = 0; i < rowAmount; i++)
            {
                var cellList = new List<Cell>();
                var random = new Random();
                for (int j = 0; j < columnAmount; j++)
                {
                    if (bombAmount > 0)
                    {
                        cellList.Add(new Cell(random.Next(100) <= 50));
                        bombAmount--;
                    }
                    else
                        cellList.Add(new Cell(false));
                }

                _cells.Add(cellList);
            }

            foreach (var cellRow in _cells)
            {
                foreach (var cell in cellRow)
                {
                    if (bombAmount == 0)
                        break;

                    if (!cell.IsBombed())
                    {
                        cell.Bomb();
                        bombAmount--;
                    }
                }

                if (bombAmount == 0)
                    break;
            }

            CheckCells();
        }

        public void CheckCellIfItIsBombed(int row, int column)
        {
            var cellToCheck = _cells[row][column];
            if (cellToCheck.IsBombed())
                _over = true;
            if (!cellToCheck.IsOpened())
                _openedCellAmount++;
            cellToCheck.Open();
            _won = _openedCellAmount >= _cells.Count * _cells[0].Count - 1 - _bombAmount;
        }

        private void CheckCells()
        {
            for (int i = 0; i < _cells.Count; i++)
                for (int j = 0; j < _cells[i].Count; j++)
                {
                    var cellToCheck = _cells[i][j];

                    if (!cellToCheck.IsBombed())
                        for (int k = -1; k < 2; k += 1)
                            for (int l = -1; l < 2; l += 1)
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

        public int GetOpenedCellAmount() => _openedCellAmount;
        public bool IsOver() => _over;
        public bool IsWon() => _won;
        public List<List<Cell>> GetCells() => _cells;
    }
}