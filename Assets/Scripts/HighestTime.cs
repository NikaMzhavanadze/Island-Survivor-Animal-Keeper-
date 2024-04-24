using UnityEngine;
using UnityEngine.UI;

public class HighestTime : MonoBehaviour
{
    public Text highestTimeText;
    public WinTimer winTimer;

    void Update()
    {
        float currentTime = Mathf.Ceil(winTimer.highestTime);
        highestTimeText.text = "Highest Time: " + currentTime + " Seconds";
    }
}
