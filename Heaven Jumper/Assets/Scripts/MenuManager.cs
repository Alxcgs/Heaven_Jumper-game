using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterShopUI characterShopUI; 
    
    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button soundButton;
    [SerializeField] private Button shopButton; 
    [SerializeField] private Button closeShopButton; 
    [SerializeField] private Sprite soundOnIcon;
    [SerializeField] private Sprite soundOffIcon;
    

    [Header("Panels")]
    [SerializeField] private GameObject characterSelectionPanel;
    [SerializeField] private GameObject shopPanel; 

    private bool isSoundOn = true;

    void Start()
    {
        
        if (characterShopUI != null)
        {
            characterShopUI.GenerateShopItemsUi();
        }
        else
        {
            Debug.LogWarning("CharacterShopUI не призначено в MenuManager!");
        }
        
        
        playButton.onClick.AddListener(StartGame);
        soundButton.onClick.AddListener(ToggleSound);
        shopButton.onClick.AddListener(OpenShop);
        closeShopButton.onClick.AddListener(CloseShop);
        
        
        int savedIndex = PlayerEconomy.GetSelectedCharacterIndex();
        if (characterShopUI != null)
        {
            characterShopUI.SelectItemUI(savedIndex);
            characterShopUI.ChangePlayerSkin(savedIndex);
        }
        
        shopPanel.SetActive(false); 

        
        isSoundOn = PlayerPrefs.GetInt("Sound", 1) == 1;
        UpdateSoundIcon();
    }

    
    void SetSelectedCharacter()
    {
        int index = PlayerEconomy.GetSelectedCharacterIndex();
        Debug.Log("Збережений індекс обраного персонажа: " + index);
    }

    
    private void OpenShop()
    {
        Debug.Log("Магазин відкрито!");
        shopPanel.SetActive(true);
    }

    
    private void CloseShop()
    {
        Debug.Log("Магазин закрито!");
        shopPanel.SetActive(false);
    }

    private void StartGame() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("Game");
    }

    private void ToggleSound() 
    {
        isSoundOn = !isSoundOn;
        AudioListener.volume = isSoundOn ? 1 : 0;
        PlayerPrefs.SetInt("Sound", isSoundOn ? 1 : 0);
        UpdateSoundIcon();
    }

    private void UpdateSoundIcon() 
    {
        soundButton.image.sprite = isSoundOn ? soundOnIcon : soundOffIcon;
    }

    public void OpenCharacterSelection() => characterSelectionPanel.SetActive(true);

    public void ClosePanels() 
    {
        characterSelectionPanel.SetActive(false);
    }
}
