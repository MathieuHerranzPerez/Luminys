using UnityEngine;
using UnityEngine.SceneManagement;

public class PreferencesMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject ui;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        Time.timeScale = 0f;                            // freeze the game
        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
        Time.timeScale = 1f;                            // unfreeze the game
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}


