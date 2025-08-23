using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleUI : MonoBehaviour
{
    [SerializeField] private Image image;

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
