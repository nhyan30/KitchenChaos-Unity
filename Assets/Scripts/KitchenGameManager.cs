using UnityEngine;
using UnityEngine.Rendering;

public class KitchenGameManager : MonoBehaviour
{
    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private State state;
    private float waitingToStartTimer = 1f;

    private void Awake()
    {
        state = State.WaitingToStart;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if(waitingToStartTimer < 0f)
                {
                   state = State.CountdownToStart;
                }
                break;
            case State.CountdownToStart:
                break;
            case State.GamePlaying:
                break;
            case State.GameOver:
                break;
        }
    }
}