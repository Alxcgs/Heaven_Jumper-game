using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    private Transform player;

    private float nextScoreHeight;
    
    private int score;
    
    public float heightInterval = 1f;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        
        nextScoreHeight = player.position.y + heightInterval;
        score = 0;
        scoreText.text = score.ToString();
    }
    
    private void Update()
    {
        if (player.position.y >= nextScoreHeight)
        {
            ScorePlus(1);
            nextScoreHeight += heightInterval;
        }
    }

    public void ScorePlus(int count)
    {
        score += count;
        scoreText.text = score.ToString();
    }
}
