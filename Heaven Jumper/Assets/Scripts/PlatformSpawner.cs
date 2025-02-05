using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;

    private void Start()
    {
        Vector3 spawnerPos = new Vector3();
        
        for (int i = 0; i < 10; i++)
        {
            spawnerPos.x = Random.Range(-2, 2);
            spawnerPos.y += Random.Range(1f, 1.5f);
            
            Instantiate(platformPrefab, spawnerPos, Quaternion.identity);
        }
    }
}
