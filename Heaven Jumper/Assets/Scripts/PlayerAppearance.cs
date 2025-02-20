using UnityEngine;

public class PlayerAppearance : MonoBehaviour
{
    
    [Header("References")]
    [SerializeField] private CharacterShopDatabase characterDB;
    
    private SpriteRenderer _spriteRenderer;
    
    
    
    void Start()
    {
        // Отримуємо компонент SpriteRenderer на гравцеві
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer == null)
        {
            Debug.LogError("PlayerAppearance: Не знайдено компонент SpriteRenderer!");
            return;
        }
        
        // Зчитуємо збережений індекс обраного персонажа
        int selectedIndex = PlayerEconomy.GetSelectedCharacterIndex();
        // Альтернативно: int selectedIndex = PlayerPrefs.GetInt("LastSelectedChar", 0);
        
        // Отримуємо дані персонажа з бази даних
        Character selectedCharacter = characterDB.GetCharacter(selectedIndex);
        if (selectedCharacter != null && selectedCharacter.image != null)
        {
            // Замінюємо спрайт гравця на спрайт обраного персонажа
            _spriteRenderer.sprite = selectedCharacter.image;
            Debug.Log("PlayerAppearance: Встановлено спрайт для персонажа: " + selectedCharacter.name);
        }
        else
        {
            Debug.LogError("PlayerAppearance: Вибраний персонаж не знайдений або не має зображення. Індекс: " + selectedIndex);
        }
        
    }
}
