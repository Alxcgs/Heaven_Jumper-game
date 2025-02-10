using UnityEngine;
using TMPro;

public class UICoinsUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsText;

    void Start()
    {
        PlayerEconomy.Instance.OnCoinsChanged += UpdateCoinsText;
        UpdateCoinsText(PlayerEconomy.Instance.Coins);
    }

    private void UpdateCoinsText(int coins)
    {
        coinsText.text = $"{coins} ☆";
    }

    void OnDestroy()
    {
        if (PlayerEconomy.Instance != null)
            PlayerEconomy.Instance.OnCoinsChanged -= UpdateCoinsText;
    }
}