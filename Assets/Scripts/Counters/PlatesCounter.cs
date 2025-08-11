using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    // to know when Plate is spwaned
    public event EventHandler OnPlateSpawned;

    // to update the amount of Plates
    public event EventHandler OnPlateRemoved;

    [SerializeField] KitchenObjectSO plateKitchenObjectSO;

    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int platesSpawnAmount;
    private int platesSpawnAmountMax = 4;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0;
            if (platesSpawnAmount < spawnPlateTimerMax)
            {
                platesSpawnAmount++;
                // fire event
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            // Player is empty handed 
            if (platesSpawnAmount > 0)
            {
                // There is at least one plate here
                platesSpawnAmount--;

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
