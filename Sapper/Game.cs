namespace Sapper
{
    public class Game
    {
        private List<List<Cell>> _cells;
        private bool _over;
        private int _openedCellAmount;

        public Game(int rowAmount, int columnAmount, int bombAmount)
        {
            _openedCellAmount = 0;
            _over = false;
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
            cellToCheck.Open();
            _openedCellAmount++;
        }

        private void CheckCells()
        {
            for (int i = 0; i < _cells.Count; i++)
                for (int j = 0; j < _cells[i].Count; j++)
                {
                    var cellToCheck = _cells[i][j];
                    for (int k = -1; k < 2; k++)
                        for (int l = -1; l < 2; l++)
                        {
                            try
                            {
                                if (_cells[i + k][j + l].IsBombed() && !(k == 0 && l == 0))
                                    cellToCheck.AddOneBombAround();
                            }
                            catch {; }
                        }
                }
        }

        public int GetOpenedCellAmount() => _openedCellAmount;
        public bool IsOver() => _over;
        public List<List<Cell>> GetCells() => _cells;
    }
}