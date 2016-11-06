using NoStackHack.Utilities;

namespace NoStackHack.ControlInput
{
    public class JumpCommand : ICommand
    {
        private float _power;
        public JumpCommand() : this(1f) { }

        public JumpCommand(float power)
        {
            _power = power;
        }
        
        public void Execute(IGameObject gameObject)
        {
            var jumper = gameObject as IJumper;
            if (jumper == null) { return; }
            jumper.Jump(_power);
        }
    }
}
