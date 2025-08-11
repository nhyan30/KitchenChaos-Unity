using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    // to know when an Ingredient is added
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    // to pass data of the event 
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjctSO)
    {
        if (!validKitchenObjectSOList.Contains(kitchenObjctSO))
        {
            // Not a valid ingredient
            return false;
        }
        if (kitchenObjectSOList.Contains(kitchenObjctSO))
        {
            // Alreadty has this ingredient
            return false;
        }
        else
        {
            kitchenObjectSOList.Add(kitchenObjctSO);

            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs{
                kitchenObjectSO = kitchenObjctSO
            });

            return true;

        }
    }
}
