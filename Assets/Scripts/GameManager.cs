using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameOver)
        {
            Debug.Log("GAME OVER"); // affD
        }
    }

    public void SetGameOver(bool over)
    {
        isGameOver = over;
    }

    public static GameManager GetInstance()
    {
        return instance;
    }
}
