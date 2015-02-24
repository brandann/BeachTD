﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Audio : MonoBehaviour {

    public static bool AudioExists;

    public AudioClip EnemyDies,
                     InGameMusic,
                     MenuMusic,
                     Alert,
                     RangedTowerShoot,
                     MeleeTowerShoot,
                     SlowTowerShoot,
                     EndMusic,
                     WinEffect,
                     LoseEffect;

    

    #region MonoBehaviour Callbacks

    void OnEnable()
    {
        Enemy.SomeEnemyDied += PlayEnemyDied;
        Global.OnGamePaused += PauseAudio;
        Global.OnGameResumed += ResumeAudio;
        MeleeTower.OnMeleeFired += PlayMeleeFired;
        RangedTower.OnRangedFired += PlayRangedFired;
        SlowTower.OnSlowFired += PlaySlowFired;

    }

   void OnDisable()
   {
       Enemy.SomeEnemyDied -= PlayEnemyDied;
       Global.OnGamePaused -= PauseAudio;
       Global.OnGameResumed -= ResumeAudio;
       MeleeTower.OnMeleeFired -= PlayMeleeFired;
       RangedTower.OnRangedFired -= PlayRangedFired;
       SlowTower.OnSlowFired -= PlaySlowFired;
   }   

	// Use this for initialization
	void Awake () {

        //Should only need one audio prefab in intial scene but this allows
        //you to put one in each scene so that you can have audio when testing without first loading menu scene
        //probably should be removed in production
        if (AudioExists)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            AudioExists = true;
        }

        AudioSource[] sources = gameObject.GetComponents<AudioSource>();
        _effectSource = sources[0];
        _musicSource = sources[1];
        _musicSource.loop = true;
        OnLevelWasLoaded(Application.loadedLevel);
       
	}

    void OnLevelWasLoaded(int lvl)
    {
        //Debug.Log("level: " + lvl);

        switch (lvl)
        {
            case 0:
                _musicSource.clip = MenuMusic;
                Debug.Log("Menu");
                break;
            case 1:
                _musicSource.clip = InGameMusic;
                break;
            case 2:
                _effectSource.PlayOneShot(LoseEffect);
                _musicSource.clip = EndMusic;
                break;
            case 3:
                _effectSource.PlayOneShot(WinEffect);
                _musicSource.clip = EndMusic;
                Debug.Log("Audio win");
                break;
            default:
                Debug.Log("No music for level");
                return;
        }

        _musicSource.Play();
    }

#endregion

    private AudioSource _effectSource;
    private AudioSource _musicSource;

    private void PauseAudio()
    {
        Debug.Log("pause audio");
        _effectSource.PlayOneShot(Alert);
        _musicSource.Pause();    

    }

    private void ResumeAudio()
    {
        Debug.Log("resume audio");
        _effectSource.PlayOneShot(Alert);
        _musicSource.Play();
    }

    private void PlayEnemyDied(Enemy e)
    {
        _effectSource.PlayOneShot(EnemyDies);
    }

    private void PlayMeleeFired()
    {
        _effectSource.PlayOneShot(MeleeTowerShoot);
    }

    private void PlaySlowFired()
    {
        _effectSource.PlayOneShot(SlowTowerShoot);
    }

    private void PlayRangedFired()
    {
        _effectSource.PlayOneShot(RangedTowerShoot);
    }
}
