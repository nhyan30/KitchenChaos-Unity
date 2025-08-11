using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    // TODO 
    // Make the Progress Bar only visible once the player interacts with it

    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image barImage;

    private IHasProgress hasProgress;

    private void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        if(hasProgress == null)
        {
            Debug.LogError("Game object " + hasProgressGameObject + " does not have a component that implements IHasProgress!");
        }
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;

        barImage.fillAmount = 0f;
        Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;

        if (e.progressNormalized == 0f || e.progressNormalized == 1.0f)
        {
            Hide();
            //if (e.progressNormalized == 1.0)
            //{
            //    StartCoroutine(BarCompleted());
            //}
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator BarCompleted()
    {
        yield return new WaitForSeconds(0.4f);
        Hide();
    }
}

