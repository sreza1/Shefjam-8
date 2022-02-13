using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip fireSound;
    public static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()

    {
        fireSound = Resources.Load<AudioClip>("sfx/laser_sfx");

        audioSrc = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PlaySound (string clip){
        switch (clip){
            case "fire":
                audioSrc.PlayOneShot(fireSound);
                break;
        }
    }
}
