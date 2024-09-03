using System;
using System.Collections.Generic;
using Player.FSM.States;
using Player.FSM.States.WalIdle;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using State = Player.FSM.States.State;

namespace Player.FSM
{
    public class PAgent : MonoBehaviour
    {
        private List<State> _states = new List<State>();
        
        private Fsm _fsm;
        [SerializeField] private InputReaderFsm inputReaderFsm;

        private Transition _walkIdleToVacuum;
        private Transition _walkIdleToWalkIdle;
        private Transition _vacuumToWalkIdle;

        [SerializeField] private WalkIdleModel walkIdleModel;

        public void Start()
        {
            inputReaderFsm.OnMove += SetMoveStateDirection;
            inputReaderFsm.OnVacuumStarted += SetVacuumState;
            inputReaderFsm.OnVacuumEnded += SetMovementState;

            State walkIdle = new WalkIdle(gameObject, walkIdleModel);
            _states.Add(walkIdle);

            State vacuum = new Vacuum();
            _states.Add(vacuum);

            _walkIdleToVacuum = new Transition() { From = walkIdle, To = vacuum };
            walkIdle.transitions.Add(_walkIdleToVacuum);
            
            _walkIdleToWalkIdle = new Transition() { From = walkIdle, To = walkIdle };
            walkIdle.transitions.Add(_walkIdleToWalkIdle);

            _vacuumToWalkIdle = new Transition() { From = vacuum, To = walkIdle };
            vacuum.transitions.Add(_vacuumToWalkIdle);

            _fsm = new Fsm(_states, walkIdle);
        }

        private void SetMoveStateDirection(Vector2 direction, bool shouldRot)
        {
            Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);

            // Apply transition only if necessary
            _fsm.ApplyTransition(_walkIdleToWalkIdle);

            bool stateFound = false;

            foreach (var state in _states)
            {
                if (_fsm.GetCurrentState() == state)
                {
                    if (state is WalkIdle walkIdle)
                    {
                        walkIdle.SetDir(moveDirection, shouldRot);
                        stateFound = true;
                        break;
                    }
                }
            }

            if (!stateFound)
            {
                Debug.Log("Current state not found in the list of states.");
            }
        }

        private void SetVacuumState()
        {
            _fsm.ApplyTransition(_walkIdleToVacuum);
        }
        
        private void SetMovementState()
        {
            _fsm.ApplyTransition(_vacuumToWalkIdle);
        }
        
        private void Update()
        {
            _fsm.Update();
        }

        private void FixedUpdate()
        {
            _fsm.FixedUpdate();
        }
    }
}