using System.Collections.Generic;
using UnityEngine;

public class SkewerCounterVisual : MonoBehaviour
{
    [SerializeField] private SkewerCounter skewerCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform skewerVisualPrefab;

    private List<GameObject> skewerVisualGameObjectList;

    private void Awake()
    {
        skewerVisualGameObjectList = new List<GameObject>();
    }

    private void Start()
    {
        skewerCounter.OnSkewerSpawned += SkewerCounter_OnSkewerSpawned;
        skewerCounter.OnSkewerRemoved += SkewerCounter_OnSkewerRemoved;
    }

    private void SkewerCounter_OnSkewerRemoved(object sender, System.EventArgs e)
    {
        GameObject skewerGameObject = skewerVisualGameObjectList[skewerVisualGameObjectList.Count - 1];
        skewerVisualGameObjectList.Remove(skewerGameObject);
        Destroy(skewerGameObject);
    }

    private void SkewerCounter_OnSkewerSpawned(object sender, System.EventArgs e)
    {
        Transform skewerVisualTransform = Instantiate(skewerVisualPrefab, counterTopPoint);

        float skewerOffsetX = .1f;
        skewerVisualTransform.localPosition = new Vector3(skewerOffsetX * skewerVisualGameObjectList.Count, 0, 0);

        skewerVisualGameObjectList.Add(skewerVisualTransform.gameObject);
    }
}
