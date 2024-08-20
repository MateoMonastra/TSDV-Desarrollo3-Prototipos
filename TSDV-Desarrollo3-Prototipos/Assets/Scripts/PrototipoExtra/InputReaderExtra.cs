using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace PrototipoExtra
{
    public class InputReaderExtra : MonoBehaviour
    {
        [Tooltip("Component for controlling running behavior")]
        public Running running; 
        public Running personaRunning; 
        public PersonaStand personaStand; 
        

        public void HandleMoveInput(InputAction.CallbackContext context)
        {
            Vector2 moveInput = context.ReadValue<Vector2>();
            Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
            
            if (!personaStand.personaInvoked)
            {
                if (running != null)
                    running.SetDir(moveDirection);
            }
            else
            {
                if (personaRunning != null)
                    personaRunning.SetDir(moveDirection);
            }
        }
    
        public void HandleInvokeInput(InputAction.CallbackContext context)
        {
            if (!context.started) return;
            
            if (personaStand.personaInvoked)
            {
                personaStand.DesInvokePersona();
            }
            else
            {
                personaStand.InvokePersona();
            }
        }
    }
}