using UnityEngine;

public class Star : MonoBehaviour
{
    public int value = 1;
    private static float _spawnOffset = 3f; //мінімальна висота над камерою 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerEconomy.Instance.AddCoins(value);
            RespawnStar();
        }

        if (other.CompareTag("DeadZone"))
        {
            RespawnStar();
        }
    }

    private void RespawnStar()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            float camWidth = mainCamera.orthographicSize * mainCamera.aspect;
            float camHeight = mainCamera.orthographicSize;
                
            float newX = Random.Range(-camWidth, camWidth);
            float newY = mainCamera.transform.position.y + camHeight + _spawnOffset; // Вища за камеру на запасну висоту

            Instantiate(gameObject, new Vector3(newX, newY, 0), Quaternion.identity);
        }

        Destroy(gameObject);
    }
}