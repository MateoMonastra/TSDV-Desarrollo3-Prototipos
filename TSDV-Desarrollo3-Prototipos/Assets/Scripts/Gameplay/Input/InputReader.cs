using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InputReader : MonoBehaviour
{
    [Tooltip("Component for controlling running behavior")]
    public Running running; 
    public VacuumCleaner vacuumCleaner;

    public void HandleMoveInput(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);

        if (running != null)
            running.SetDir(moveDirection);
    }
    
    public void HandleVacuumCleanerInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            
        }
    }
}