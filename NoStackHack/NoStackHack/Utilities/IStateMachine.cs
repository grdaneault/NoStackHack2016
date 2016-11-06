using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoStackHack.Utilities
{
    public interface IState<TArg> where TArg : class
    {
        void Init();
        IState<TArg> Update(TArg arg);
        void Exit();
    }

    public class StateMachine<TArg> where TArg : class
    {
        public IState<TArg> ActiveState { get; private set; }

        private bool _firstPass = true;

        public StateMachine(IState<TArg> initialState)
        {
            ActiveState = initialState;
        }

        public void Update(TArg arg)
        {
            if (_firstPass)
            {
                _firstPass = false;
                ActiveState.Init();
            }

            var next = ActiveState.Update(arg);

            if (next != null)
            {
                ActiveState.Exit();
                next.Init();
                ActiveState = next;
            }
        }
    }
}
