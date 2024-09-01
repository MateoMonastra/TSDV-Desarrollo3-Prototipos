using System;
using Player.FSM.States;

namespace Player.FSM
{
    public class Transition 
    {
        public State From;
        public State To;
        
        public event Action TransitionAction;
    
        public void Do()
        {
            From.Exit();
            TransitionAction.Invoke();
            To.Enter();
        }
    }
}
