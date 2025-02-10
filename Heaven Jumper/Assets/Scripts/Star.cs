using UnityEngine;

public class Star : MonoBehaviour
{
    public int value = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerEconomy.Instance.AddCoins(value);
            Destroy(gameObject);
        }

        if (other.CompareTag("DeadZone"))
        {
            Destroy(gameObject);
        }
    }
}