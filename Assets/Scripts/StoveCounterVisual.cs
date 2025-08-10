using Unity.VisualScripting;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveGameObject;
    [SerializeField] private GameObject particlesGameObject;

    private void Start()
    {
        // Listen to the event
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        stoveGameObject.SetActive(showVisual);
        particlesGameObject.SetActive(showVisual);
    }
}
