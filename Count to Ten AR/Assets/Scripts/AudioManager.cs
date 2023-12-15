using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip[] correctMatch;
    [SerializeField] AudioClip[] wrongMatch;
    [SerializeField] AudioClip congrats;
    [SerializeField] AudioClip intro;
    [SerializeField] AudioClip halfWay;
    public static AudioManager Instance;
    AudioSource audioSource;
    
    private void Awake() {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    public void PlayIntro()
    {
        audioSource.PlayOneShot(intro, 1f);
    }
    public void PlayCongrats()
    {
        if(!audioSource.isPlaying)
        audioSource.PlayOneShot(congrats, 1f);

        else if(audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(congrats, 1f);
        }
    }
    public void PlayWrongMatch()
    {
        if(!audioSource.isPlaying)
        audioSource.PlayOneShot(wrongMatch[Random.Range(0, wrongMatch.Length)], 1f);
    }
    public void PlayCorrectMatch()
    {
        if(!audioSource.isPlaying)
        audioSource.PlayOneShot(correctMatch[Random.Range(0, correctMatch.Length)], 1f);
    }
    public void PlayHalfWay()
    {
        if(!audioSource.isPlaying)
        audioSource.PlayOneShot(halfWay, 1f);

        else if(audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(halfWay, 1f);
        }
    }
}
