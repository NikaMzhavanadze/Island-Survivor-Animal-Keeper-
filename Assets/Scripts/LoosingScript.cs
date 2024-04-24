using UnityEngine;
using UnityEngine.SceneManagement;

public class LoosingScript : MonoBehaviour
{
    public PlayerScript playerController;
    public PauseScript pauseScript;
    public AudioManager audioManager;
    public CanvasGroup wasted;
    public Animator anim;
    public float animalCount;
    bool playDeathEffect = true;
    bool canPlayAnimation = true;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        animalCount = GameObject.FindGameObjectsWithTag("Animal").Length;

        if (animalCount == 0 || playerController.health <= 0)
        {
            if (Time.timeScale > 0)
            {
                SlowMotion();
            }
            if (Time.timeScale <= 0)
            {
                playerController.enabled = false;
            }
        }

        if (animalCount == 0 || playerController.health <= 0)
        {
            audioManager.musicSource.volume -= Time.deltaTime / 30;
            wasted.alpha += Time.deltaTime;
            if (playDeathEffect)
            {
                audioManager.PlayDeathSound();
                if (canPlayAnimation)
                {
                    anim.SetBool("Death", canPlayAnimation);
                    //canPlayAnimation = false;
                }
                anim.SetBool("Death", canPlayAnimation);
                playDeathEffect = false;
            }

            if (audioManager.musicSource.volume <= 0)
            {
                audioManager.musicSource.Stop();
            }
        }
        if (Time.timeScale == 0 && Input.anyKeyDown && pauseScript.isPaused == false)
        {
            SceneManager.LoadScene("Test");
            Time.timeScale = 1;
        }
    }
    public void SlowMotion()
    {
        Time.timeScale -= Time.deltaTime / 1.2f;
        if (Time.timeScale <= 0.021f)
        {
            Time.timeScale = 0;
        }
    }
}
