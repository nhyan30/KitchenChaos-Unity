using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject()) // Counter is Empty
        {
            // There is no KitchenObject here 
            if (player.HasKitchenObject())
            {
                // Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
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
                //Player is carrying somthing
            }
            else
            {
                //Player not carrying somthing
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
