using UnityEngine;

public class GrillCounterVisual : MonoBehaviour
{
    [SerializeField] private GrillCounter grillCounter;
    [SerializeField] private GameObject stoveGameObject;
    [SerializeField] private GameObject particlesGameObject;

    private void Start()
    {
        // Listen to the event
        grillCounter.OnStateChangedGrilling += GrillCounter_OnStateChanged;
    }

    private void GrillCounter_OnStateChanged(object sender, GrillCounter.OnStateChangedGrillingEventArgs e)
    {
        bool showVisual = e.state == GrillCounter.State.Grilling || e.state == GrillCounter.State.Grilled;
        stoveGameObject.SetActive(showVisual);
        particlesGameObject.SetActive(showVisual);
    }
}
