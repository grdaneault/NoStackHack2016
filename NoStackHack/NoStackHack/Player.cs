using NoStackHack.Utilities;

namespace NoStackHack
{
    [GameEntity]
    public class Player
    {
        private int _x, _y;

        public Player() { }

        public Player(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public void Jump()
        {
            _y += 10;
        }
    }
}
