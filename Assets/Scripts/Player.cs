using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void Update()
    {
        HandleMovements();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if(selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void HandleInteractions()
    {
        Vector3 inputVector = gameInput.GetMovementVectorNormalized();

        if (inputVector != Vector3.zero)
        {
            lastInteractDir = inputVector;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit rayCastHit, interactDistance, countersLayerMask))
        {
            if (rayCastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                // Has clear Counter
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }
    
    
    private void HandleMovements()
    {
        Vector3 inputVector = gameInput.GetMovementVectorNormalized();
        float moveDistance = Time.deltaTime * moveSpeed;
        float rotationSpeed = 10f;
        float playerRadius = .7f;
        float playerHeight = 2f;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, inputVector, moveDistance);
        if (!canMove)
        {
            // Cannot move towards movement

            // Attempt only X movement
            Vector3 moveDirX = new Vector3(inputVector.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                // Can move only on the X
                inputVector = moveDirX;
            }
            else
            {
                // Can't move only on the X
                
                // Attempt only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, inputVector.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    // Can move only on the Z
                    inputVector = moveDirZ;
                }
            }
        }

        if (canMove)
        {
            transform.position += inputVector * Time.deltaTime * moveSpeed;
        }
        isWalking = inputVector != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, inputVector, rotationSpeed * Time.deltaTime);
    }
    
    private void SetSelectedCounter(BaseCounter selctedCounters)
    {
        this.selectedCounter = selctedCounters;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchechenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}