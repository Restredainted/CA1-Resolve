using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip menuClickClip;
    public AudioClip jumpClip;
    public AudioClip footStepClip;
    public AudioClip backgroundMusic;
    public AudioClip fireballClip;
    public AudioClip combatMusic;
    public AudioClip attackClip;

    private AudioSource soundEffectSource;
    private AudioSource backgroundMusicSource;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) {

            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else {

            Destroy(gameObject);
            return;
        }

        soundEffectSource = gameObject.AddComponent<AudioSource>();
        backgroundMusicSource = gameObject.AddComponent<AudioSource>();
        

        backgroundMusicSource.clip = backgroundMusic;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.Play();
    }

    public void playMenuClick() {
    soundEffectSource.PlayOneShot(menuClickClip);
    }

    public void playJumpClip() {
        soundEffectSource.PlayOneShot(jumpClip);
    }

    public void playFootstepClip() {
        soundEffectSource.PlayOneShot(footStepClip);
    }

    public void playAttackClip() {
        soundEffectSource.PlayOneShot(attackClip);
    }

    public void playfireballClip(Transform tf) {
        soundEffectSource.PlayOneShot(jumpClip);
    }

    public void playBackgroundMusic(bool combat) {

        if (combat) {
            backgroundMusicSource.clip = combatMusic;
        }
        else {
            backgroundMusicSource.clip = backgroundMusic;
        }

        if (!backgroundMusicSource.isPlaying)
            backgroundMusicSource.Play();
    }
    public AudioSource playFireBall() {
        var fireball = Instantiate<AudioSource>(this.soundEffectSource);
        fireball.clip = fireballClip;
        fireball.loop = true;
        fireball.Play();
        return fireball;

    }


    public void stopFireball(AudioSource fireball) {
        fireball.Stop();
        
    }

    public void setFXVolume (float volume) {
        soundEffectSource.volume = volume;
    }

    public void setBGMVolume (float volume) {
        backgroundMusicSource.volume = volume;
    }



}
