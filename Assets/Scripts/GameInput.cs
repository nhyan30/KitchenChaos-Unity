using UnityEngine;

public class GameInput : MonoBehaviour
{
    PlayerInputActions playerInputActions;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }
    public Vector3 GetMovementVectorNormalized()
    {
        Vector3 inputVector = playerInputActions.Player.Move.ReadValue<Vector3>();

        inputVector = inputVector.normalized;
        return inputVector;
    }
}
