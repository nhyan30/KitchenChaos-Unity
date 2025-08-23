using UnityEngine;

public class GrillCounterVisual : MonoBehaviour
{
    [SerializeField] private GrillCounter grillCounter;
    [SerializeField] private GameObject stoveGameObject;
    [SerializeField] private GameObject particlesGameObject;

    private void Start()
    {
        // Listen to the event
        grillCounter.OnStateChangedFrying += GrillCounter_OnStateChanged;
    }

    private void GrillCounter_OnStateChanged(object sender, GrillCounter.OnStateChangedFryingEventArgs e)
    {
        bool showVisual = e.state == GrillCounter.State.Grilling;
        stoveGameObject.SetActive(showVisual);
        particlesGameObject.SetActive(showVisual);
    }
}
