using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button soundButton;
    [SerializeField] private Sprite soundOnIcon;
    [SerializeField] private Sprite soundOffIcon;

    [Header("Panels")]
    [SerializeField] private GameObject characterSelectionPanel;
    [SerializeField] private GameObject shopPanel;

    private bool isSoundOn = true;

    
    void Start()
    {
        playButton.onClick.AddListener(StartGame);
        soundButton.onClick.AddListener(ToggleSound);
        
        // Завантажити налаштування звуку з PlayerPrefs
        isSoundOn = PlayerPrefs.GetInt("Sound", 1) == 1;
        UpdateSoundIcon();
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

    // Викликати через кнопки
    public void OpenCharacterSelection() => characterSelectionPanel.SetActive(true);
    public void OpenShop() => shopPanel.SetActive(true);
    public void ClosePanels()
    {
        characterSelectionPanel.SetActive(false);
        shopPanel.SetActive(false);
    }
}