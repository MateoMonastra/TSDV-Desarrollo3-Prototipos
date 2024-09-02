using System;
using System.Collections.Generic;
using Player.FSM.States;
using Player.FSM.States.WalIdle;
using Unity.VisualScripting;
using UnityEngine;
using State = Player.FSM.States.State;

namespace Player.FSM
{
    public class PAgent : MonoBehaviour
    {
        private Fsm _fsm;
        private InputReaderFsm _inputReaderFsm;

        private Transition _walkIdleToVacuum;
        private Transition _vacuumToWalkIdle;

        [SerializeField] private WalkIdleModel walkIdleModel;

        public void Start()
        {
            _inputReaderFsm.OnMove += SetMoveStateDirection;

            List<State> states = new List<State>();

            State walkIdle = new WalkIdle(gameObject, walkIdleModel);
            states.Add(walkIdle);

            State vacuum = new Vacuum();
            states.Add(vacuum);

            _walkIdleToVacuum = new Transition() { From = walkIdle, To = vacuum };
            walkIdle.transitions.Add(_walkIdleToVacuum);

            _vacuumToWalkIdle = new Transition() { From = vacuum, To = walkIdle };
            vacuum.transitions.Add(_vacuumToWalkIdle);

            _fsm = new Fsm(states, walkIdle);
        }

        private void SetMoveStateDirection(Vector2 direction, bool shouldRot)
        {
            Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);

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