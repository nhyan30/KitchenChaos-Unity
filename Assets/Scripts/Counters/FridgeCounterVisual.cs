using UnityEngine;

public class FridgeCounterVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";

    [SerializeField] private FridgeCounter fridgeCounter;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        // Listen to the Event
        fridgeCounter.OnPlayerGrabbedObject += FridgeCounter_OnPlayerGrabbedObject;
    }

    private void FridgeCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
