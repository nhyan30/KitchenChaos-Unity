using UnityEngine;

[CreateAssetMenu()]
public class GrillingRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float grillingTimerMax;
}
