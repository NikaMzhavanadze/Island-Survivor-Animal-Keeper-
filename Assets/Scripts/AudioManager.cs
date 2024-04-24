using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public LoosingScript loosingScript;
    [SerializeField] float musicSoundVolume;
    [SerializeField] float VFXSoundVolume;
    [Header("----------Audio Source--------")]
    public AudioSource musicSource;
    public AudioSource SFXSource;
    public AudioSource heartBeatSource;
    public AudioSource deathSource;
    public AudioSource looseSource;
    public AudioSource pickUpSource;
    public AudioSource hit;

    [Header("----------Audio clips--------")]
    public AudioClip background;
    public AudioClip heartBeat;
    public AudioClip[] walking;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.volume = musicSoundVolume / 50;
        musicSource.Play();
    }
    public void Hit()
    {
        hit.Play();
    }        
    public void PickUpSource()
    {
        pickUpSource.Play();
    }    
    public void Death()
    {
        deathSource.Play();
    }    
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayHeartBeat()
    {
        heartBeatSource.Play();
    }

    public void StopHeartBeat()
    {
        heartBeatSource.Stop();
    }

    public void PlayDeathSound()
    {
        looseSource.Play();
    }
}
