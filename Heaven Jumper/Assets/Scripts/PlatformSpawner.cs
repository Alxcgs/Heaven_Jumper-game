using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [Header("Main Settings")]
    public GameObject platformPrefab;
    public GameObject specialCloudPrefab; // Префаб сірої хмари
    public GameObject starPrefab;
    
    [Space(10)]
    [Range(5, 10)] public int platformsPerStar = 7;
    public float yLevelTolerance = 1.5f;

    [Header("Special Cloud Settings")] 
    [Range(0.01f, 0.2f)] public float specialCloudChance = 0.1f; // 10% шанс
    public float minVerticalDistanceBetweenSpecial = 8f; // Мінімальна відстань між сірими хмарами

    private Vector3 _spawnerPos;
    private int _platformCounter;
    private float _lastStarY = -Mathf.Infinity;
    private float _lastSpecialCloudY = -Mathf.Infinity; // Для контролю відстані

    void Start()
    {
        _spawnerPos = Vector3.zero;
        for (int i = 0; i < 10; i++) SpawnPlatform();
    }

    public void SpawnNewPlatform()
    {
        SpawnPlatform();
    }
    void SpawnPlatform()
    {
        _spawnerPos.x = Random.Range(-2f, 2f);
        _spawnerPos.y += Random.Range(0.5f, 1.5f);

        GameObject platformToSpawn = platformPrefab;
        
        // Перевірка умов для спавну сірої хмари
        if (CanSpawnSpecialCloud())
        {
            platformToSpawn = specialCloudPrefab;
            _lastSpecialCloudY = _spawnerPos.y; // Запам'ятовуємо позицію
        }

        Instantiate(platformToSpawn, _spawnerPos, Quaternion.identity);
        
        _platformCounter++;

        if (_platformCounter >= platformsPerStar)
        {
            SpawnStar();
            _platformCounter = 0;
            platformsPerStar = Random.Range(5, 11);
        }
    }

    bool CanSpawnSpecialCloud()
    {
        return Random.value < specialCloudChance && 
               Mathf.Abs(_spawnerPos.y - _lastSpecialCloudY) > minVerticalDistanceBetweenSpecial;
    }

    public void SpawnStar()
    {
        Vector3 starPos = _spawnerPos + new Vector3(Random.Range(-1f, 1f), 1f, 0);
        Instantiate(starPrefab, starPos, Quaternion.identity);
        _lastStarY = starPos.y;
    }
}