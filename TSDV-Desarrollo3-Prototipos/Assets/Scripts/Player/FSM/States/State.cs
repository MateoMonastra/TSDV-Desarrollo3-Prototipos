using System;
using System.Collections.Generic;

namespace Player.FSM.States
{
    public abstract class State
    {
        public List<Transition> transitions = new();
        public Action OnEnter;
        public Action OnTick;
        public Action OnFixedTick;
        public Action OnExit;

        public virtual void Enter() => OnEnter?.Invoke();

        public virtual void Tick(float delta) => OnTick?.Invoke();

        public virtual void FixedTick(float delta) => OnFixedTick?.Invoke();

        public virtual void Exit() => OnExit?.Invoke();

        public bool TryGetTransition(Transition transition)
        {
            foreach (var transitionCandidate in transitions)
            {
                if (transitionCandidate == transition)
                {
                    return true;
                }
            }

            return false;
        }
    }
}