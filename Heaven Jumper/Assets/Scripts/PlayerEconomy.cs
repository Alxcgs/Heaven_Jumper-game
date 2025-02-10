using UnityEngine;
using System;

public class PlayerEconomy : MonoBehaviour
{
    public static PlayerEconomy Instance;
    public event Action<int> OnCoinsChanged;

    public int Coins => PlayerPrefs.GetInt("Coins", 0);

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Зберігає об'єкт між сценами
            LoadInitialData(); // Завантажити дані при ініціалізації
        }
        else
        {
            Destroy(gameObject); // Видалити дублікат
        }
    }

    private void LoadInitialData()
    {
        // Додаткова логіка завантаження даних (якщо потрібно)
        PlayerPrefs.Save(); // Гарантуємо збереження даних
    }

    public void AddCoins(int amount)
    {
        int newCoins = Coins + amount;
        PlayerPrefs.SetInt("Coins", newCoins);
        PlayerPrefs.Save();
        OnCoinsChanged?.Invoke(newCoins); // Сповістити про зміну балансу
    }

    public void SpendCoins(int amount)
    {
        int newCoins = Mathf.Max(0, Coins - amount);
        PlayerPrefs.SetInt("Coins", newCoins);
        PlayerPrefs.Save();
        OnCoinsChanged?.Invoke(newCoins);
    }
}