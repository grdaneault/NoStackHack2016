using Microsoft.Xna.Framework;

namespace NoStackHack.Utilities
{
    public class StateMachine<TState> where TState : class, IState
    {
        public TState ActiveState { get; private set; }

        private bool _firstPass = true;

        public StateMachine(TState initialState)
        {
            ActiveState = initialState;
        }

        public void Update(GameTime gameTime)
        {
            if (_firstPass)
            {
                _firstPass = false;
                ActiveState.Init();
            }

            var next = ActiveState.Update(gameTime) as TState;

            if (next != null)
            {
                ActiveState.Exit();
                next.Init();
                ActiveState = next;
            }
        }
    }
}
