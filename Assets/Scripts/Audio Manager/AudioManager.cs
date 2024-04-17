using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----------Audio Source----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----------Audio Clip For Background----------")]
    public AudioClip background;

    [Header("----------Audio Clip For CrowDeadth----------")]
    public AudioClip crowdeathDetect1;
    public AudioClip crowdeathDetect2;
    public AudioClip crowdeathDetect3;
    public AudioClip crowdeathDeath;
    public AudioClip crowdeathAttack;

    [Header("----------Audio Clip For Player----------")]
    public AudioClip playerFight;
    public AudioClip playerKick;
    public AudioClip EntryPortal;
    private void Start(){
        musicSource.clip = background;
        musicSource.Play();
    }
    public void PlaySFX(AudioClip clip){
        SFXSource.PlayOneShot(clip);
    }
}
