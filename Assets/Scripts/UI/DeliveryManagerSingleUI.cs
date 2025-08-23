using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeSO(RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.recipeName;

        foreach(Transform child in iconContainer)
        {
            if(child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            foreach (Sprite sprite in kitchenObjectSO.sprite)
            {
                Transform iconTransform = Instantiate(iconTemplate, iconContainer);
                iconTransform.gameObject.SetActive(true);

                Image iconImage = iconTransform.GetComponent<Image>();
                iconImage.sprite = sprite;
            }
        }
    }
}
