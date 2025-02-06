using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    
    private Rigidbody2D _rb;
    [SerializeField] private GameObject gameMenuPanel; 

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        _rb.linearVelocity = new Vector2(horizontalInput * speed, _rb.linearVelocity.y);
        
        GetComponent<SpriteRenderer>().flipX = _rb.linearVelocity.x > 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DeadZone")) 
        {
            gameMenuPanel.SetActive(true);
            Time.timeScale = 0f; 
        }
    }
}