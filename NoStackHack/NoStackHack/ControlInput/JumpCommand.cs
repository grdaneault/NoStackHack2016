using NoStackHack.Utilities;

namespace NoStackHack.ControlInput
{
    public class JumpCommand : ICommand
    {
        public void Execute(IGameObject gameObject)
        {
            var jumper = gameObject as IJumper;
            if (jumper == null) { return; }
            jumper.Jump();
        }
    }
}
