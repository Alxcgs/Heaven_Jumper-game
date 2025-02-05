using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public float speed = 5f;
    
    private Rigidbody2D _rb;
    
    
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        _rb.linearVelocity = new Vector2(horizontalInput * speed, _rb.linearVelocity.y);
        
        if (_rb.linearVelocity.x < 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        if (_rb.linearVelocity.x > 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
              
        

        //додати поворот персонажу за допомогою flipX
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("DeadZone"))
        {
            SceneManager.LoadScene("Game");
        }
    }
}
