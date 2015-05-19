using UnityEngine;
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
                     LoseEffect,
                     ClickEffect;

    

    #region MonoBehaviour Callbacks

    void OnEnable()
    {
        Enemy.SomeEnemyDied += PlayEnemyDied;
        Global.OnGamePaused += PauseAudio;
        Global.OnGameResumed += ResumeAudio;
        MeleeTower.OnMeleeFired += PlayMeleeFired;
        RangedTower.OnRangedFired += PlayRangedFired;
        SlowTower.OnSlowFired += PlaySlowFired;
        ClickableUI.OnButtonClicked += PlayClick;
    }

   void OnDisable()
   {
       Enemy.SomeEnemyDied -= PlayEnemyDied;
       Global.OnGamePaused -= PauseAudio;
       Global.OnGameResumed -= ResumeAudio;
       MeleeTower.OnMeleeFired -= PlayMeleeFired;
       RangedTower.OnRangedFired -= PlayRangedFired;
       SlowTower.OnSlowFired -= PlaySlowFired;
       ClickableUI.OnButtonClicked -= PlayClick;
   }   

	// Use this for initialization
	void Awake () {
        //Debug.Log("Audio Awake");

        //Should only need one audio _powPrefab in intial scene but this allows
        //you to put one in each scene so that you can have audio when testing without first loading menu scene
        //probably should be removed in production
        if (AudioExists)
        {
            //Debug.Log("Audio destroy self");
            Destroy(gameObject);
            _beingDestroyed = true;
            return;
        }
        else
        {
            //Debug.Log("don't destroy");
            DontDestroyOnLoad(transform.gameObject);
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
        //Destroy has been called but we're still in the same frame
        if (_beingDestroyed)
            return;

        //Debug.Log("level: " + lvl);

        switch (lvl)
        {
            case 0:
                _musicSource.clip = MenuMusic;
                break;
            case 1:
                _musicSource.clip = InGameMusic;
                break;
            case 2:
                _effectSource.PlayOneShot(LoseEffect);
                _musicSource.clip = InGameMusic;
                break;
            case 3:
                _effectSource.PlayOneShot(WinEffect);
                _musicSource.clip = InGameMusic;
                //Debug.Log("Audio win");
                break;
            default:
                Debug.LogError("No music for level");
                return;
        }

        _musicSource.Play();
    }

#endregion

    private AudioSource _effectSource;
    private AudioSource _musicSource;
    private bool _beingDestroyed;

    private void PauseAudio()
    {
        //Debug.Log("pause audio");
        _effectSource.PlayOneShot(Alert);
        _musicSource.Pause();    

    }

    private void ResumeAudio()
    {
        //Debug.Log("resume audio");
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

    private void PlayClick()
    {
        _effectSource.PlayOneShot(ClickEffect);
    }
}
