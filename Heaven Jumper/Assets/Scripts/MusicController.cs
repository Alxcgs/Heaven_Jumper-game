using UnityEngine;

public class MusicController : MonoBehaviour
{
    private static MusicController _instance; // Для запобігання дублювання

    void Awake()
    {
        // Якщо об'єкт вже існує, знищити новий
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject); // Робимо об'єкт незнищуваним
    }
}