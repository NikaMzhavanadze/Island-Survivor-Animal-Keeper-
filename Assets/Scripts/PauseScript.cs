using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public bool isPaused;
    public PlayerScript playerController;
    public GameObject pausePanel;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
        isPaused = false;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
        playerController.enabled = true;
    }

    public void Menu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Test");
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void ResetTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        //print(Time.timeScale);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Time.timeScale = 0.0f;
                isPaused = true;
                pausePanel.SetActive(true);
                playerController.enabled = false;
            }
            else if (isPaused)
            {
                Time.timeScale = 1.0f;
                isPaused = false;
                pausePanel.SetActive(false);
                playerController.enabled = true;
            }
        }
    }
}
