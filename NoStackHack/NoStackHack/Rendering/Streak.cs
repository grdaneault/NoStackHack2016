using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Microsoft.Xna.Framework;

namespace NoStackHack.Rendering
{
    class Streak
    {

        public Point Start { get; private set; }
        public Vector2 Top { get; private set; }
        private Point _size;
        private Vector2 _speed;
        private int _weight;
        private readonly int _maxLength;

        private Color _color;

        public Color Color
        {
            get { return _color; }
            private set { _color = Jitter(value); }

        }

        private static Random rand = new Random(10);
        private static Color Jitter(Color s)
        {
            return new Color(s.R, s.G, s.B, 255);
        }

        public Streak(Point start, int maxLength, int speed)
        {
            
            Start = start;
            Top = start.ToVector2();
            _maxLength = maxLength;
            _speed = Vector2.UnitY * speed;
            _size = new Point(_weight, maxLength);

            if (_speed.Y <= 4)
            {
                Color = new Color(65, 125, 150);
                _weight = 1;
            }
            else if (_speed.Y <= 10)
            {
                Color = new Color(88, 157, 205);
                _weight = 2;
            }
            else
            {
                Color = new Color(161, 225, 255);
                _weight = 4;
            }
        }

        public Rectangle Step()
        {
            Top = Top + _speed;
            return new Rectangle(Top.ToPoint(), _size);
        }

        public void Loop(Rectangle screenSize)
        {
            if (Top.Y > screenSize.Height)
            {
                Top = new Vector2(Top.X, -_maxLength);
                _size = new Point(_weight, _maxLength);
            }
        }
    }
}
