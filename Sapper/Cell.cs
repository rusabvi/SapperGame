namespace Sapper
{
    public class Cell
    {
        private bool _bombed;
        private bool _opened;
        private int _bombsAroundAmount;

        public Cell(bool bombed)
        {
            _bombed = bombed;
            _opened = false;
            if (_bombed)
                _bombsAroundAmount = 10;
            else
                _bombsAroundAmount = 0;
        }

        public bool IsBombed() => _bombed;
        public bool IsOpened() => _opened;
        public int GetBombsAroundAmount() => _bombsAroundAmount;

        public void Open() => _opened = true;
        public void AddOneBombAround() => _bombsAroundAmount++;

        public void Bomb()
        {
            _bombed = true;
            _bombsAroundAmount = 10;
        }
    }
}