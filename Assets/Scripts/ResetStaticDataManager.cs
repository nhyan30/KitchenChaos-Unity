using UnityEngine;

/// <summary>
/// Object lifetime and statics, static fields dont manually get cleaned up 
/// </summary>

public class ResetStaticDataManager : MonoBehaviour
{
    private void Awake()
    {
        CuttingCounter.ResetStaticData(); 
        BaseCounter.ResetStaticData();
        TrashCounter.ResetStaticData();
    }

}
