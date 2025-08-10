using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CounterSelectedVisual : MonoBehaviour
{
    [SerializeField] MeshRenderer[] meshRenderers;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] private Color emissionColor = new Color(1.3f, 1.3f, 1.3f); // Light gray

    [SerializeField] private BaseCounter baseCounter;

    private MaterialPropertyBlock propertyBlock;

    private void Start()
    {
        propertyBlock = new MaterialPropertyBlock();
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if(e.selectedCounter == baseCounter)
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
        foreach (var meshRenderer in meshRenderers)
        {
            meshRenderer.GetPropertyBlock(propertyBlock);
            propertyBlock.SetColor("_EmissionColor", emissionColor);
            meshRenderer.SetPropertyBlock(propertyBlock);
        }

        if(spriteRenderer != null)
        {
            spriteRenderer.GetPropertyBlock(propertyBlock);
            propertyBlock.SetColor("_EmissionColor", emissionColor);
            spriteRenderer.SetPropertyBlock(propertyBlock);
        }
    }

    private void Hide()
    {
        foreach (var meshRenderer in meshRenderers)
        {
            meshRenderer.GetPropertyBlock(propertyBlock);
            propertyBlock.SetColor("_EmissionColor", Color.black);
            meshRenderer.SetPropertyBlock(propertyBlock);
        }

        if(spriteRenderer != null)
        {
            spriteRenderer.GetPropertyBlock(propertyBlock);
            propertyBlock.SetColor("_EmissionColor", Color.black);
            spriteRenderer.SetPropertyBlock(propertyBlock);
        }
    }
}
