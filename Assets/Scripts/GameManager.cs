using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(GameOver))]
public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public AnimationCurve curve;

    [SerializeField]
    private BlockWaveControler blockWaveControler;

    private GameOver gameOver;
    private bool isGameOver = false;

    private float score;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        gameOver = GetComponent <GameOver>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetGameOver()
    {
        score = blockWaveControler.GetChrono();
        StartCoroutine(SlowGame());
    }

    public IEnumerator SlowGame()
    {
        float t = 0f;
        while (t < 0.8f)
        {
            t += Time.deltaTime;
            float slow = curve.Evaluate(t);
            Time.timeScale = slow;
            yield return 0;                             // skip to the next frame
        }

        GameOver();
    }


    private void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;                            // freeze the game

        gameOver.GameOverFunc(score);
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    public void Restart()
    {
        Time.timeScale = 1f;                            // unfreeze the game
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadMenuScene()
    {
        Time.timeScale = 1f;                            // unfreeze the game
        SceneManager.LoadScene("MainMenu");
    }
}
