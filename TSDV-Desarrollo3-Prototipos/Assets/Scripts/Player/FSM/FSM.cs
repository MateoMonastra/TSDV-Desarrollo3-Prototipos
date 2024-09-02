using System.Collections.Generic;
using Player.FSM.States;
using UnityEngine;

namespace Player.FSM
{
    public class Fsm
    {
        private List<State> _states;

        public State Current;

        public Fsm(List<State> states, State current)
        {
            _states = states;
            Current = current;
        }

        public void Update()
        {
            Current.Tick(Time.deltaTime);
        }
        
        public void FixedUpdate()
        {
            Current.Tick(Time.deltaTime);
        }
   
        public void ApplyTransition(Transition transition)
        {
            if (transition == null) return;

            if (Current.TryGetTransition(transition))
            {
                transition.From.Exit();
                transition.To.Enter();
            }
        }
    }
}
