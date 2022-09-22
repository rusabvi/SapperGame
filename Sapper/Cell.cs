namespace Sapper
{
    public class Cell
    {
        private bool _bombed;
        private bool _opened;
        private int _bombAroundAmount;

        internal Cell(bool bombed)
        {
            _opened = false;
            _bombAroundAmount = 0;
            _bombed = bombed;
            _opened = false;
        }

        public CellValue GetValue()
        {
            if (!_opened)
                return CellValue.Unknown;

            if (_bombed)
                return CellValue.Bomb;

            return (CellValue)_bombAroundAmount;
        }

        internal void Open() => _opened = true;
        internal void AddOneBombAround() => _bombAroundAmount++;
        internal void Bomb() => _bombed = true;

        internal bool IsBombed() => _bombed;
    }
}