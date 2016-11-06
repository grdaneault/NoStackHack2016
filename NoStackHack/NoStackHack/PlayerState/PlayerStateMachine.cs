

using NoStackHack.Utilities;

namespace NoStackHack.PlayerState
{
    public class PlayerStateMachine : StateMachine<IPlayerState>
    {
        public PlayerStateMachine(IPlayerState initialState) : base(initialState)
        {
        }
    }
}
