using System;
using UnityEngine;

public class GrillCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedFryingEventArgs> OnStateChangedFrying;
    public class OnStateChangedFryingEventArgs : EventArgs
    {
        public State state;
    }
    public enum State
    {
        Idle,
        Grilling,
        Grilled,
    }

    [SerializeField] private GrillingRecipeSO[] grillingRecipeSOArray;

    private float grillingTimer;
    private GrillingRecipeSO grillingRecipeSO;
    private State state;

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Grilling:
                    grillingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = grillingTimer / grillingRecipeSO.grillingTimerMax
                    });

                    if (grillingTimer > grillingRecipeSO.grillingTimerMax)
                    {
                        // Fried
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(grillingRecipeSO.output, this);

                        // Object Fried

                        state = State.Grilled;

                        // fire the Event ,passes the state
                        OnStateChangedFrying?.Invoke(this, new OnStateChangedFryingEventArgs
                        {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                    break;
                case State.Grilled:
                    break;
            }
            // Debug.Log(state);
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject()) // Counter is Empty
        {
            // There is no KitchenObject here 
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    // Player carying somthing that can be Fried
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    grillingRecipeSO = GetGrillingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    state = State.Grilling;
                    grillingTimer = 0f;

                    // fire the Event ,passes the state
                    OnStateChangedFrying?.Invoke(this, new OnStateChangedFryingEventArgs
                    {
                        state = state
                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = grillingTimer / grillingRecipeSO.grillingTimerMax
                    });
                }
            }
            else
            {
                // Player not carrying anything
            }
        }
        else // Counter is not Empty
        {
            // There is KitchenObject here 
            if (player.HasKitchenObject())
            {
                //Player is carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // Player is holding a Plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();

                        state = State.Idle;

                        // fire the Event ,passes the state
                        OnStateChangedFrying?.Invoke(this, new OnStateChangedFryingEventArgs
                        {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                }
            }
            else
            {
                //Player not carrying something
                GetKitchenObject().SetKitchenObjectParent(player);

                state = State.Idle;

                // fire the Event ,passes the state
                OnStateChangedFrying?.Invoke(this, new OnStateChangedFryingEventArgs
                {
                    state = state
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }
        }
    }

    // Check if the KitchenObject can be Fried (!Vegs & Bread)
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        GrillingRecipeSO grillingRecipeSO = GetGrillingRecipeSOWithInput(inputKitchenObjectSO);
        return grillingRecipeSO != null;
    }

    private GrillingRecipeSO GetGrillingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (GrillingRecipeSO grillingRecipeSO in grillingRecipeSOArray)
        {
            if (grillingRecipeSO.input == inputKitchenObjectSO)
            {
                return grillingRecipeSO;
            }
        }
        return null;
    }

    public bool IsGrilled()
    {
        return state == State.Grilled;
    }
}
