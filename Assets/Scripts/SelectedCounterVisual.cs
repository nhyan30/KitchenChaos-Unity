using UnityEngine;

public class CounterSelectedVisual : MonoBehaviour
{
    [SerializeField] Material normalMaterial;
    [SerializeField] Material actvivatedMaterial;
    [SerializeField] MeshRenderer meshRenderer;

    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if(e.selectedCounter == clearCounter)
        {
            Show();
        }
        else 
        {
            Hide();
        }
    }

    private void Show()
    {
        meshRenderer.material = actvivatedMaterial;
    }

    private void Hide()
    {
        meshRenderer.material = normalMaterial;

    }
}
