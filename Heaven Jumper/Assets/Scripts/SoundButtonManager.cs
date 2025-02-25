using UnityEngine;
using UnityEngine.UI;

public class SoundButtonManager : MonoBehaviour
{
    [SerializeField] Sprite soundOn, soundOff; 
    // Компонент Image кнопки (для зміни іконки)
    Image _buttonImage;

    void Start()
    {
        // Отримуємо компонент Image з цього об'єкта (кнопки)
        _buttonImage = GetComponent<Image>();
        // Додаємо обробник події натискання кнопки: викликатиметься метод Toggle()
        GetComponent<Button>().onClick.AddListener(Toggle);
        UpdateIcon();
    }

    void Toggle()
    {
        // Отримуємо AudioSource з незнищуваного об'єкта
        AudioSource music = GameObject.Find("MusicManager").GetComponent<AudioSource>();
        music.mute = !music.mute;
        UpdateIcon();
    }

    void UpdateIcon()
    {
        AudioSource music = GameObject.Find("MusicManager").GetComponent<AudioSource>();
        // Встановлюємо іконку: якщо звук увімкнено - soundOn, інакше - soundOff
        _buttonImage.sprite = music.mute ? soundOn : soundOff;
    }
}