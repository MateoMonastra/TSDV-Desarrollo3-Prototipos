using System;
using System.Collections.Generic;
using Player.FSM.States;
using UnityEngine;

namespace Player.FSM
{
    public class P_Agent : MonoBehaviour
    {
        private FSM _Fsm;
        public void Awake()
        {
            List<State> states = new();
            
            State walkIdle = new WalkIdle();
            states.Add( walkIdle );
            
            State vacuum = new Vacuum();
            states.Add( vacuum );

            var walkIdleToVacuum = new Transition() { From = walkIdle, To = vacuum };
            walkIdle.transitions.Add(walkIdleToVacuum);

            var vacuumToWalkIdle = new Transition() { From = vacuum, To = walkIdle };
            vacuum.transitions.Add(vacuumToWalkIdle);
        }
    }
}
