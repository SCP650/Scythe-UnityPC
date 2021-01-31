using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioSource scytheSwingSound;
    [SerializeField]
    AudioSource deathSound;
    [SerializeField]
    AudioSource soulBlinkSound;
    [SerializeField]
    AudioSource dashSound;
    [SerializeField]
    AudioSource dashRecoverSound;

    public static SoundManager S;

    private void Awake()
    {
        if (S == null) S = this;
        else Destroy(this.gameObject);
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySwingSound()
    {
        if (scytheSwingSound.isPlaying) scytheSwingSound.Stop();
        scytheSwingSound.Play();
    }

    public void PlayDeathSound()
    {
        if (scytheSwingSound.isPlaying) deathSound.Stop();
        deathSound.Play();
    }

    public void PlaySoulBlinkSound()
    {
        soulBlinkSound.Play();
    }

    public void StopSoulBlinkSound()
    {
        soulBlinkSound.Stop();
    }

    public void PlayDashSound()
    {
        if (dashSound.isPlaying) dashSound.Stop();
        dashSound.Play();
    }

    public void PlayDashRecoverSound()
    {
        if (dashRecoverSound.isPlaying) dashRecoverSound.Stop();
        dashRecoverSound.Play();
    }
}
