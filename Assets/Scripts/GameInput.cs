using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPuaseAction;

    private PlayerInputActions playerInputActions;


    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy() 
    {
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;

        playerInputActions.Dispose(); // clean up the object and free up any memory 
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPuaseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
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
