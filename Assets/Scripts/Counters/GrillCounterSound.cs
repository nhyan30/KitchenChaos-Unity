using UnityEngine;

public class GrillCounterSound : MonoBehaviour
{
    [SerializeField] private GrillCounter grillCounter;

    private AudioSource audioSource;
    private float warningSoundTimer;
    private bool playWarningSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        grillCounter.OnStateChangedFrying += GrillCounter_OnStateChangedFrying;
        grillCounter.OnProgressChanged += GrillCounter_OnProgressChanged;
    }

    private void GrillCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = .5f;
        playWarningSound = grillCounter.IsGrilled() && e.progressNormalized >= burnShowProgressAmount;
    }

    private void GrillCounter_OnStateChangedFrying(object sender, GrillCounter.OnStateChangedFryingEventArgs e)
    {
        bool playSound = e.state == GrillCounter.State.Grilling;
        if (playSound) { audioSource.Play(); }
        else { audioSource.Pause(); }
    }

    private void Update()
    {
        if (playWarningSound)
        {
            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer <= 0f)
            {
                float warningSoundTimerMax = .2f;
                warningSoundTimer = warningSoundTimerMax;

                SoundManager.Instance.PlayWarningSound(grillCounter.transform.position);
            }
        }
    }
}
