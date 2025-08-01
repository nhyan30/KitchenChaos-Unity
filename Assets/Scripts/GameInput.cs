using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction; 
    private PlayerInputActions playerInputActions;


    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        // Until we have listeners to the Event

        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector3 GetMovementVectorNormalized()
    {
        Vector3 inputVector = playerInputActions.Player.Move.ReadValue<Vector3>();

        inputVector = inputVector.normalized;
        return inputVector;
    }
}
