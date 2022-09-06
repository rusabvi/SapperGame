namespace Sapper
{
    public class Game
    {
        private List<List<Cell>> _cells;
        private bool _over;

        public Game(byte rowAmount, byte columnAmount)
        {
            _over = false;
            _cells = new List<List<Cell>>();
            for (byte i = 0; i < rowAmount; i++)
            {
                var cellList = new List<Cell>();
                var random = new Random();
                for (byte j = 0; j < columnAmount; j++)
                    cellList.Add(new Cell(random.Next(100) <= 50));
                _cells.Add(cellList);
            }

            CheckCells();
        }

        public void CheckCellIfItIsBomb(byte row, byte column)
        {
            var cellToCheck = _cells[row][column];
            if (cellToCheck.IsBombed())
                _over = true;
            cellToCheck.Open();
            CheckCells();
        }

        private void CheckCells()
        {
            for (short i = 0; i < _cells.Count; i++)
                for (short j = 0; j < _cells[i].Count; j++)
                {
                    var cellToCheck = _cells[i][j];
                    for (sbyte k = -1; k < 2; k++)
                        for (sbyte l = -1; l < 2; l++)
                        {
                            try
                            {
                                if (_cells[i + k][j + l].IsBombed())
                                    cellToCheck.AddBombAround();
                            }
                            catch {; }
                        }
                }
        }

        public bool IsOver() => _over;
        public List<List<Cell>> GetCells() => _cells;
    }
}