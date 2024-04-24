using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeatSound : MonoBehaviour
{
    public Animator healthAnim;
    private PlayerScript playerScript;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.health <= 50 && playerScript.health > 30)
        {
            healthAnim.SetBool("Half", true);
            healthAnim.SetBool("Low", false);
        }
        else if (playerScript.health <= 30 && playerScript.health > 0)
        {
            healthAnim.SetBool("Low", true);
            healthAnim.SetBool("Half", false);
        }
    }
}