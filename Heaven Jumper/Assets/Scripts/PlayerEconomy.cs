using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class PlayerEconomy : MonoBehaviour
{
    public static PlayerEconomy Instance;
    public event Action<int> OnCoinsChanged;
    
    // Гроші
    public int Coins => PlayerPrefs.GetInt("Coins", 0);
    
    // Куплені персонажі
    public List<int> PurchasedCharacters => _purchasedCharacters;
    private List<int> _purchasedCharacters = new List<int>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadPurchasedCharacters(); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    public static int GetSelectedCharacterIndex() => 
        PlayerPrefs.GetInt("LastSelectedChar", 0);

    public static void SetSelectedCharacterIndex(int newIndex) => 
        PlayerPrefs.SetInt("LastSelectedChar", newIndex);

    
    public void AddPurchasedCharacter(int index)
    {
        if (!_purchasedCharacters.Contains(index))
        {
            _purchasedCharacters.Add(index);
            SavePurchasedCharacters();
        }
    }

    private void SavePurchasedCharacters() => 
        PlayerPrefs.SetString("PurchasedChars", string.Join(",", _purchasedCharacters));

    private void LoadPurchasedCharacters()
    {
        var data = PlayerPrefs.GetString("PurchasedChars", "");
        if (!string.IsNullOrEmpty(data))
            _purchasedCharacters = data.Split(',').Select(int.Parse).ToList();
    }

    // Логіка монет
    public void AddCoins(int amount)
    {
        var newCoins = Coins + amount;
        PlayerPrefs.SetInt("Coins", newCoins);
        OnCoinsChanged?.Invoke(newCoins);
    }

    public void SpendCoins(int amount)
    {
        var newCoins = Mathf.Max(0, Coins - amount);
        PlayerPrefs.SetInt("Coins", newCoins);
        OnCoinsChanged?.Invoke(newCoins);
    }
}