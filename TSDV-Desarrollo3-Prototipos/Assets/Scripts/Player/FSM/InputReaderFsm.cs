using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.FSM
{
    public class InputReaderFsm : MonoBehaviour
    {
        public Action<Vector2,bool> OnMove;
        public Action OnVacuumStarted;
        public Action OffVacuumEnded;

        public void HandleMoveInput(InputAction.CallbackContext context)
        {
            OnMove?.Invoke(context.ReadValue<Vector2>(),Input.GetMouseButton(0));
        }

        public void HandleVacuumCleanerInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                OnVacuumStarted.Invoke();
            }
            else if (context.canceled)
            {
                OffVacuumEnded.Invoke();
            }
        }
    }
}
