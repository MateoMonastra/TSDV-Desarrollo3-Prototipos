using System.Collections.Generic;
using Player.FSM.States;
using UnityEngine;

namespace Player.FSM
{
    public class Fsm
    {
        private List<State> _states;

        private State _current;

        public Fsm(List<State> states, State current)
        {
            _states = states;
            _current = current;
        }

        public void Update()
        {
            _current.Tick(Time.deltaTime);
            Debug.Log(_current);
        }

        public void FixedUpdate()
        {
            _current.FixedTick(Time.deltaTime);
        }

        public void ApplyTransition(Transition transition)
        {
            if (transition == null) return;
            if (!_current.TryGetTransition(transition)) return;
            
            transition.From.Exit();
            transition.To.Enter();
            _current = transition.To;
        }

        public State GetCurrentState()
        {
            return _current;
        }
    }
}