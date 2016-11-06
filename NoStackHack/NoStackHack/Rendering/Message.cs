using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace NoStackHack.Rendering
{
    class Message
    {
        private Func<string> _updateMessage;
        private Func<Vector2> _updateLocation;
        public Vector2 Location => _updateLocation();
        public string Text => _updateMessage();

        public Message(Vector2 location, string message) : this(() => location, () => message) { }

        public Message(Func<Vector2> location, string message) : this(location, () => message) { }

        public Message(Vector2 location, Func<string> message) : this(() => location, message) { }

        public Message(Func<Vector2> location, Func<string> message)
        {
            _updateMessage = message;
            _updateLocation = location;
        }
    }
}
