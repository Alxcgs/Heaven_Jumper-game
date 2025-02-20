using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class CharacterItemUI : MonoBehaviour
{
    [Header("Appearance Settings")]
    [SerializeField] Color baseColor;         // Базовий колір елемента
    [SerializeField] Color selectedColor;     // Яскравіший варіант базового кольору (при виборі)
    [SerializeField] Color outlineColor = Color.white; // Колір контуру (світлий)

    [Header("UI Elements")]
    [SerializeField] Image characterImage;      // Зображення персонажа
    [SerializeField] TMP_Text characterName;    // Назва персонажа
    [SerializeField] TMP_Text characterPrice;   // Ціна персонажа
    [SerializeField] Button purchaseButton;       // Кнопка покупки

    [Space(20f)]
    [SerializeField] Button itemButton;         // Кнопка для вибору елемента
    [SerializeField] Image itemImage;      // Фон елемента (на ньому відображається базовий колір)
    [SerializeField] Outline itemOutline;       // Контур елемента

    // Встановлюємо позицію елемента у контейнері
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
    
    // Викликається, коли персонажа куплено – ховаємо кнопку покупки й встановлюємо базовий вигляд
    public void SetCharacterAsPurchased()
    {
        purchaseButton.gameObject.SetActive(false);
        itemButton.interactable = true;
        SetBaseAppearance();
    }
    
    // Налаштовуємо обробку натискання для покупки
    public void OnItemPurchased(int itemIndex, UnityAction<int> action)
    {
        purchaseButton.onClick.RemoveAllListeners();
        purchaseButton.onClick.AddListener(() => action.Invoke(itemIndex));
    }
    
    // Налаштовуємо обробку натискання для вибору елемента
    public void OnItemSelect(int itemIndex, UnityAction<int> action)
    {
        itemButton.interactable = true;
        itemButton.onClick.RemoveAllListeners();
        itemButton.onClick.AddListener(() => action.Invoke(itemIndex));
    }
    
    // Встановлюємо базовий вигляд: базовий колір, без контуру
    private void SetBaseAppearance()
    {
        if(itemOutline != null)
            itemOutline.enabled = false;
        itemButton.interactable = true;
    }
    
    // Відображаємо елемент як вибраний: світлий контур і яскравіший фон
    public void SelectItem()
    {
        if(itemOutline != null)
        {
            itemOutline.enabled = true;
            itemOutline.effectColor = outlineColor;
        }
        itemButton.interactable = false;
    }
    
    // Повертаємо елемент до базового вигляду
    public void DeselectItem()
    {
        SetBaseAppearance();
    }
}
