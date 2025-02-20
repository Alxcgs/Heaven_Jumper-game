using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    private Transform _player;

    private float _nextScoreHeight;
    
    private int _score;
    
    public float heightInterval = 1f;
    
    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
        
        _nextScoreHeight = _player.position.y + heightInterval;
        _score = 0;
        scoreText.text = _score.ToString();
    }
    
    private void Update()
    {
        if (_player.position.y >= _nextScoreHeight)
        {
            ScorePlus(1);
            _nextScoreHeight += heightInterval;
        }
    }

    private void ScorePlus(int count)
    {
        _score += count;
        scoreText.text = _score.ToString();
    }
}
