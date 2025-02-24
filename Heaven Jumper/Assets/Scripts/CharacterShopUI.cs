using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CharacterShopUI : MonoBehaviour
{
    [Header("Layout Settings")]
    [SerializeField] float itemSpacing = 0.5f;
    float _itemHeight;

    [Header("UI Elements")]
    [SerializeField] Image selectedCharacterIcon;
    [SerializeField] Transform shopMenu;
    [SerializeField] Transform shopItemsContainer;
    [SerializeField] GameObject itemPrefab;

    [Space(20)]
    [SerializeField] CharacterShopDatabase characterDB;

    [Space(20)]
    [Header("Main Menu")]
    [SerializeField] Image mainMenuCharacterIcon;
    [SerializeField] TMP_Text mainMenuCharacterName;

    [Space(20)]
    [Header("Purchase FX & Error Message")]
    [SerializeField] ParticleSystem purchaseFX;
    [SerializeField] TMP_Text noEnoughCoinsText;

    int _newSelectedItemIndex;
    int _previousSelectedItemIndex;

    public void GenerateShopItemsUi()
    {
        // Помічаємо всі куплені персонажі в базі даних, використовуючи дані з PlayerEconomy
        foreach (int purchasedCharacterIndex in PlayerEconomy.Instance.PurchasedCharacters)
        {
            characterDB.PurchaseCharacter(purchasedCharacterIndex);
        }

        // Отримуємо висоту шаблонного елемента, видаляємо його та від'єднуємо дочірні елементи
        _itemHeight = shopItemsContainer.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
        Destroy(shopItemsContainer.GetChild(0).gameObject);
        shopItemsContainer.DetachChildren();

        // Генеруємо UI-елементи для всіх персонажів
        for (int i = 0; i < characterDB.CharactersCount; i++)
        {
            Character character = characterDB.GetCharacter(i);
            CharacterItemUI uiItem = Instantiate(itemPrefab, shopItemsContainer)
                                        .GetComponent<CharacterItemUI>();

            uiItem.SetItemPosition(Vector2.down * i * (_itemHeight + itemSpacing));
            uiItem.gameObject.name = "Item" + i + "-" + character.name;

            uiItem.SetCharacterName(character.name);
            uiItem.SetCharacterImage(character.image);
            uiItem.SetCharacterPrice(character.price);

            if (character.isPurchased)
            {
                uiItem.SetCharacterAsPurchased();
                uiItem.OnItemSelect(i, OnItemSelected);
            }
            else
            {
                uiItem.SetCharacterPrice(character.price);
                uiItem.OnItemPurchased(i, OnItemPurchased);
            }

            // Оновлюємо розмір контейнера (можна зробити один раз після циклу, але тут – як в оригіналі)
            shopItemsContainer.GetComponent<RectTransform>().sizeDelta =
                Vector2.up * ((_itemHeight + itemSpacing) * characterDB.CharactersCount + itemSpacing);
        }

        // Локальні функції для обробки вибору та покупки

        void OnItemSelected(int index)
        {
            SelectItemUi(index);

            Character selectedCharacter = characterDB.GetCharacter(index);
            if (selectedCharacter != null && selectedCharacter.image != null)
            {
                selectedCharacterIcon.sprite = selectedCharacter.image;
            }
            else
            {
                Debug.LogError("Вибраний персонаж не знайдений або не має зображення. Індекс: " + index);
            }

            PlayerEconomy.SetSelectedCharacterIndex(index);
            PlayerPrefs.Save();

            ChangePlayerSkin(index);

            Debug.Log("Вибраний персонаж з індексом: " + index);
        }

        void SelectItemUi(int itemIndex)
        {
            _previousSelectedItemIndex = _newSelectedItemIndex;
            _newSelectedItemIndex = itemIndex;

            CharacterItemUI newUiItem = GetItemUI(_newSelectedItemIndex);
            CharacterItemUI prevUiItem = GetItemUI(_previousSelectedItemIndex);

            prevUiItem.DeselectItem();
            newUiItem.SelectItem();
        }

        CharacterItemUI GetItemUI(int index)
        {
            return shopItemsContainer.GetChild(index).GetComponent<CharacterItemUI>();
        }

        void OnItemPurchased(int index)
        {
            purchaseFX.Play();
            // Отримуємо дані персонажа та UI-елемент
            Character character = characterDB.GetCharacter(index);
            CharacterItemUI uiItem = GetItemUI(index);

            // Зчитуємо поточну кількість монет з PlayerPrefs
            int currentCoins = PlayerPrefs.GetInt("Coins", 0);

            if (currentCoins >= character.price)
            {
                // Purchase FX
                purchaseFX.Play();
                // Витрачаємо монети за допомогою PlayerEconomy
                PlayerEconomy.Instance.SpendCoins(character.price);
                // Позначаємо персонажа як купленого в базі даних
                characterDB.PurchaseCharacter(index);
                // Оновлюємо UI елемента: ховаємо кнопку покупки, активуємо кнопку вибору
                uiItem.SetCharacterAsPurchased();
                uiItem.OnItemSelect(index, OnItemSelected);
                // Реєструємо куплений персонаж у даних
                PlayerEconomy.Instance.AddPurchasedCharacter(index);
                Debug.Log("Персонаж куплено: " + character.name);
            }
            else
            {
                noEnoughCoinsText.transform.DOComplete();
                noEnoughCoinsText.DOComplete();
                

                noEnoughCoinsText.transform.DOShakeScale(0.5f, new Vector3(0.3f,0f,0f),3,0);
                noEnoughCoinsText.DOFade(1f, 0.3f).From(0f).OnComplete(() =>
                {
                    noEnoughCoinsText.DOFade(0f, 0.5f);
                });
                
                Debug.LogWarning("Недостатньо монет для покупки персонажа: " + character.name);
                
                uiItem.AnimateShakeItem();
            }
        }
    }

    public void ChangePlayerSkin(int index)
    {
        Character selectedCharacter = characterDB.GetCharacter(index);
        if (selectedCharacter != null && selectedCharacter.image != null)
        {
            mainMenuCharacterIcon.sprite = selectedCharacter.image;
            mainMenuCharacterName.text = selectedCharacter.name;
            selectedCharacterIcon.sprite = selectedCharacter.image;
        }
        else
        {
            Debug.LogError("ChangePlayerSkin: Персонаж не знайдений або не має зображення. Індекс: " + index);
        }
    }

    public void SelectItemUI(int index)
    {
        _previousSelectedItemIndex = _newSelectedItemIndex;
        _newSelectedItemIndex = index;

        CharacterItemUI newUiItem = shopItemsContainer.GetChild(_newSelectedItemIndex).GetComponent<CharacterItemUI>();
        CharacterItemUI prevUiItem = shopItemsContainer.GetChild(_previousSelectedItemIndex).GetComponent<CharacterItemUI>();

        prevUiItem.DeselectItem();
        newUiItem.SelectItem();
    }
}
