using UnityEngine;
using UnityEngine.UI;

public class WinTimer : MonoBehaviour
{
    private float Time = 0;
    public float highestTime = 0;
    private bool isRunning = true;
    public Text timerText;

    public LoosingScript loosingScript;
    public PlayerScript playerScript;

    public bool isPlaying = true;

    void Start()
    {
        highestTime = PlayerPrefs.GetFloat("HighestTime", 0f);
    }

    public void Exit()
    {
        isPlaying = false;
    }

    void Update()
    {
        float currentTime = Mathf.Ceil(Time);
        timerText.text = "Time: " + currentTime;
        if (isRunning)
        {
            Time += UnityEngine.Time.deltaTime;
            Debug.Log("Current Time: " + Time + " Seconds");
        }

        if (loosingScript.animalCount == 0 || playerScript.health <= 0 || isPlaying == false)
        {
            if (Time > highestTime)
            {
                highestTime = Time;
                PlayerPrefs.SetFloat("HighestTime", highestTime);
            }
            Debug.Log("Highest Time: " + highestTime);
        }
    }

    void OnApplicationQuit()
    {
        // Save highest time to PlayerPrefs when quitting the application
        PlayerPrefs.SetFloat("HighestTime", highestTime);
    }
}
