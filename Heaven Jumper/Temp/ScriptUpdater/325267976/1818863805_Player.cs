using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    
    private Rigidbody2D _rb;
    [SerializeField] private GameObject gameMenuPanel; // Посилання на панель меню

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        _rb.linearVelocity = new Vector2(horizontalInput * speed, _rb.linearVelocity.y);
        
        if (_rb.linearVelocity.x < 0)
            GetComponent<SpriteRenderer>().flipX = false;
        if (_rb.linearVelocity.x > 0)
            GetComponent<SpriteRenderer>().flipX = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("DeadZone"))
        {
            // Відкрити Game Menu замість перезапуску гри
            gameMenuPanel.SetActive(true);
            Time.timeScale = 0f; // Пауза гри
        }
    }

    // Функція для кнопки "Почати знову"
    public void RestartGame()
    {
        Time.timeScale = 1f; // Відновлення часу
        SceneManager.LoadScene("Game");
    }

    // Функція для кнопки "Вийти в головне меню"
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}