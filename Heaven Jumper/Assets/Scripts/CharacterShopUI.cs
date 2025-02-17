using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterShopUI : MonoBehaviour
{
    [Header("Layout Settings")]
    [SerializeField] float itemSpacing = 0.5f;
    float itemHeight;
    
    [Header("UI Elements")]
    [SerializeField] Image selectedCharacterIcon;
    [SerializeField] Transform ShopMenu;
    [SerializeField] Transform ShopItemsContainer;
    [SerializeField] GameObject itemPrefab;

    [Space(20)] 
    [SerializeField] CharacterShopDatabase characterDB;
    
    [Space(20)]
    [Header("Main Menu")]
    [SerializeField] Image mainMenuCharacterIcon;
    [SerializeField] TMP_Text mainMenuCharacterName;
    
    int newSelectedItemIndex = 0;
    int previousSelectedItemIndex = 0;

    public void GenerateShopItemsUi()
    {
        
        itemHeight = ShopItemsContainer.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
        Destroy(ShopItemsContainer.GetChild(0).gameObject);
        ShopItemsContainer.DetachChildren();
        
        
        for (int i = 0; i < characterDB.CharactersCount; i++)
        {
            Character character = characterDB.GetCharacter(i);
            CharacterItemUI uiItem = Instantiate(itemPrefab, ShopItemsContainer).GetComponent<CharacterItemUI>();
            
            
            uiItem.SetItemPosition(Vector2.down * i * (itemHeight + itemSpacing));
            
            
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
            
            
            ShopItemsContainer.GetComponent<RectTransform>().sizeDelta =
                Vector2.up * ((itemHeight + itemSpacing) * characterDB.CharactersCount + itemSpacing);

            
            void OnItemSelected(int index)
            {
                
                SelectItemUi(index);

                
                Character selectedCharacter = characterDB.GetCharacter(index);
                if(selectedCharacter != null && selectedCharacter.image != null)
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
                previousSelectedItemIndex = newSelectedItemIndex;
                newSelectedItemIndex = itemIndex;

                CharacterItemUI newUiItem = getItemUI(newSelectedItemIndex);
                CharacterItemUI prevUiItem = getItemUI(previousSelectedItemIndex);
                
                prevUiItem.DeselectItem();
                newUiItem.SelectItem();
            }
            
            CharacterItemUI getItemUI(int index)
            {
                return ShopItemsContainer.GetChild(index).GetComponent<CharacterItemUI>();
            }
            
            void OnItemPurchased(int index)
            {
                Debug.Log("Item purchased: " + index);
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
         previousSelectedItemIndex = newSelectedItemIndex;
         newSelectedItemIndex = index;

         CharacterItemUI newUiItem = ShopItemsContainer.GetChild(newSelectedItemIndex).GetComponent<CharacterItemUI>();
         CharacterItemUI prevUiItem = ShopItemsContainer.GetChild(previousSelectedItemIndex).GetComponent<CharacterItemUI>();

         prevUiItem.DeselectItem();
         newUiItem.SelectItem();
    }
}
