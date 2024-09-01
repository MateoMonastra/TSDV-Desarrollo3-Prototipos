using System.Collections.Generic;
using Player.FSM.States;
using UnityEngine;

namespace Player.FSM
{
    public class FSM : MonoBehaviour
    {
        List<State> states;

        State current;

        public FSM(List<State> states)
        {
            this.states = states;
        }

        public void Update()
        {
            current.Tick(Time.deltaTime);
        }
   
        public void ApplyTransition(Transition transition)
        {
            if (transition == null) return;
            
            transition.From.Exit();
            transition.To.Enter();
        }
    }
}
