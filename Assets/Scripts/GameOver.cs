using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text bestScoreText;
    private float currentScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOverFunc(float score)
    {
        currentScore = score;
        scoreText.text = string.Format("{0:00.0}", currentScore);

        float bestScore = PlayerPrefs.GetFloat("bestScore", currentScore);
        if (bestScore < currentScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetFloat("bestScore", bestScore);
        }
        bestScoreText.text = "best : " + string.Format("{0:00.0}", bestScore);
        gameOverUI.SetActive(true);
    }
}
