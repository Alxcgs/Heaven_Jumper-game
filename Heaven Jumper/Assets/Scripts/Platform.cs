using System;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float forceJump;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            
            // Перевіряємо чи гравець рухався вниз
            if (playerRb != null && collision.relativeVelocity.y <= 0)
            {
                // Скидаємо вертикальну швидкість для стабільного стрибка
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 0);
                
                // Додаємо силу імпульсом
                playerRb.AddForce(Vector2.up * forceJump, ForceMode2D.Impulse);
            }
        }
    }
}
