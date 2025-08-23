using System;
using UnityEngine;

public class SkewerCounter : BaseCounter
{
    // to know when Skewer is spwaned
    public event EventHandler OnSkewerSpawned;

    // to update the amount of Skewers
    public event EventHandler OnSkewerRemoved;

    [SerializeField] private KitchenObjectSO skewerKitchenObjectSO;

    private float spawnSkewerTimer;
    private float spawnSkewerTimerMax = 4f;
    private int skewerSpawnAmount;
    private int skewerSpawnAmountMax = 3;

    private void Update()
    {
        spawnSkewerTimer += Time.deltaTime;
        if (spawnSkewerTimer > spawnSkewerTimerMax)
        {
            spawnSkewerTimer = 0f;
            if (KitchenGameManager.Instance.IsGamePlaying() && skewerSpawnAmount < skewerSpawnAmountMax)
            {
                skewerSpawnAmount++;
                // fire event
                OnSkewerSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            // Player is empty handed 
            if (skewerSpawnAmount > 0)
            {
                // There is at least one skewer here
                skewerSpawnAmount--;

                KitchenObject.SpawnKitchenObject(skewerKitchenObjectSO, player);
                OnSkewerRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
