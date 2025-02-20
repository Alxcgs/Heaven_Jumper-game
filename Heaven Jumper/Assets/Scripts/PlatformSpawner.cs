using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject starPrefab;
    [Range(5, 10)] public int platformsPerStar = 7; // 1 зірка на 5-10 платформ
    public float yLevelTolerance = 1.5f; // Мінімальна відстань між зірками по Y

    private Vector3 _spawnerPos;
    private int _platformCounter;
    private float _lastStarY = -Mathf.Infinity;

    void Start()
    {
        _spawnerPos = new Vector3(0,0,0);
        for (int i = 0; i < 10; i++) SpawnPlatform();
    }

    void SpawnPlatform()
    {
        _spawnerPos.x = Random.Range(-2, 2);
        _spawnerPos.y += Random.Range(0.5f, 1.5f);
        Instantiate(platformPrefab, _spawnerPos, Quaternion.identity);

        _platformCounter++;

        // Генеруємо зірку кожні 5-10 платформ
        if (_platformCounter >= platformsPerStar && 
            Mathf.Abs(_spawnerPos.y - _lastStarY) > yLevelTolerance)
        {
            SpawnStar();
            _platformCounter = 0;
            platformsPerStar = Random.Range(5, 11); // Випадкове значення для наступної зірки
        }
    }

    void SpawnStar()
    {
        Vector3 starPos = _spawnerPos + new Vector3(Random.Range(-1f, 1f), 1f, 0);
        Instantiate(starPrefab, starPos, Quaternion.identity);
        _lastStarY = starPos.y; // Запам'ятовуємо Y-позицію останньої зірки
    }
}