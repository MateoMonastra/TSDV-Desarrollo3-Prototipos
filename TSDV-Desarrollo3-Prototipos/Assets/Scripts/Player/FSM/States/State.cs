using System;
using System.Collections.Generic;

namespace Player.FSM.States
{
    public abstract class State
    {
        public List<Transition> transitions = new();
        public Action OnEnter;
        public Action OnTick;
        public Action OnExit;

        public virtual void Enter()
            => OnEnter.Invoke();

        public virtual void Tick(float delta)
            => OnTick.Invoke();

        public virtual void Exit()
            => OnExit.Invoke();

        private bool TryGetTransition(State toCandidate,
            out Transition transition)
        {
            foreach (var transitionCandidate in transitions)
            {
                if (transitionCandidate.To == toCandidate)
                {
                    transition = transitionCandidate;
                    return true;
                }
            }

            transition = null;
            return false;
        }
    }
}