using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObjcet))
            {
                // Only accepts Plates
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
