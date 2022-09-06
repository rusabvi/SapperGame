namespace Sapper
{
    public class Cell
    {
        private readonly bool _bombed;
        private bool _opened;
        private byte _bombsAroundAmount;

        public Cell(bool bombed)
        {
            _bombed = bombed;
            _opened = false;
            if (_bombed)
                _bombsAroundAmount = 9;
            else
                _bombsAroundAmount = 0;
        }

        public bool IsBombed() => _bombed;
        public bool IsOpened() => _opened;
        public byte GetBombsAroundAmount() => _bombsAroundAmount;

        public void SetBombsAroundAmount(byte bombAmount) => _bombsAroundAmount = bombAmount;
        public void Open() => _opened = true;

        public void AddBombAround() => _bombsAroundAmount++;
    }
}