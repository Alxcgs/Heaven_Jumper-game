using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
public class CharacterItemUI : MonoBehaviour
{
    [SerializeField] Color itemNotSelectedColor;
    [SerializeField] Color itemSelectedColor;

    
    
    [Space (20f)]
    [SerializeField] Image characterImage;
    [SerializeField] TMP_Text characterName;
    [SerializeField] TMP_Text characterPrice;
    [SerializeField] Button purchaseButton;
    
    [Space (20f)]
    [SerializeField] Button itemButton;
    [SerializeField] Image itemImage;
    [SerializeField] Outline itemOutline;
    
    
    public void SetItemPosition(Vector2 position)
    {
        GetComponent<RectTransform>().anchoredPosition += position;
    }
    
    public void SetCharacterImage(Sprite sprite)
    {
        characterImage.sprite = sprite;
    }
    
    public void SetCharacterName(string name)
    {
        characterName.text = name;
    }
    
    public void SetCharacterPrice(int price)
    {
        characterPrice.text = price.ToString();
    }
    
    public void SetCharacterAsPurchased()
    {
        purchaseButton.gameObject.SetActive(false);
        itemButton.interactable = true;
        
        itemImage.color = itemNotSelectedColor;
    }
    
    public void OnItemPurchased(int itemIndex, UnityAction<int> action)
    {
        purchaseButton.onClick.RemoveAllListeners();
        purchaseButton.onClick.AddListener(() => action.Invoke(itemIndex));
    }
    
    public void OnItemSelect (int itemIndex, UnityAction<int> action)
    {
        itemButton.interactable = true;
        itemButton.onClick.RemoveAllListeners();
        itemButton.onClick.AddListener(() => action.Invoke(itemIndex));
    }
    
    public void SelectItem()
    {
        itemOutline.enabled = true;
        itemImage.color = itemSelectedColor;
        itemButton.interactable = false;
    }
    
    public void DeselectItem()
    {
        itemOutline.enabled = false;
        itemImage.color = itemNotSelectedColor;
        itemButton.interactable = true;
    }

}
