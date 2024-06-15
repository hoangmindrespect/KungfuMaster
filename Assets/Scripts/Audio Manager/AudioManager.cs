using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("----------Audio Source----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----------Audio Clip For Background----------")]
    public AudioClip background;
    public AudioClip combatboss;

    [Header("----------Audio Clip For CrowDeadth----------")]
    public AudioClip crowdeathDetect1;
    public AudioClip crowdeathDetect2;
    public AudioClip crowdeathDetect3;
    public AudioClip crowdeathDeath;
    public AudioClip crowdeathAttack;

    [Header("----------Audio Clip For Minotour----------")]
    public AudioClip minotourDetect;
    public AudioClip minotourDeath;
    public AudioClip minotourAttack;

    [Header("----------Audio Clip For Player----------")]
    public AudioClip playerFight;
    public AudioClip playerKick;
    public AudioClip EntryPortal;

    [Header("----------Audio Clip For Collecting Gem----------")]
    public AudioClip collectingGem;

    private void Start(){
        if(SceneManager.GetActiveScene().name == "Game"){
            musicSource.clip = background;
            musicSource.Play();
        }else if(SceneManager.GetActiveScene().name == "Boss1"){
            musicSource.clip = combatboss;
            musicSource.Play();
        }
    }
    public void PlaySFX(AudioClip clip){
        SFXSource.PlayOneShot(clip);
    }
}
