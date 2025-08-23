using UnityEngine;

public class GrillBurnFlashingBarUI : MonoBehaviour
{
    private const string IS_FLASHING = "IsFlashing";

    [SerializeField] private GrillCounter grillCounter;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        grillCounter.OnProgressChanged += GrillCounter_OnProgressChanged;

        animator.SetBool(IS_FLASHING, false);
    }

    private void GrillCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = .5f;
        bool show = grillCounter.IsGrilled() && e.progressNormalized >= burnShowProgressAmount;

        animator.SetBool(IS_FLASHING, show);
    }
}
