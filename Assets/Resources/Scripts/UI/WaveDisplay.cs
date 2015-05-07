using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveDisplay : MonoBehaviour {

    private int _totalWaves;
    private int _currentWave;
    private Text _text;

	// Use this for initialization
	void Awake () {

        _currentWave = 1;
        _totalWaves = GameObject.Find("Global").GetComponent<Global>().WaveCount;
        _text = gameObject.GetComponent<Text>();
        UpdateDisplay();
	}    

    void OnEnable()
    {
        EnemyManager.OnWaveStarted += HandleWaveStart;
        UpdateDisplay();
    }

    void OnDisable()
    {
        EnemyManager.OnWaveStarted -= HandleWaveStart;
    }

    void HandleWaveStart()
    {
        Debug.Log("Wave start");
        _currentWave++;
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        //hackish way to deal with fact that wave count may not have been initialized before awake

        // can we move it OnEnable()? <- Brandan
        if (_totalWaves == 0)
            _totalWaves = GameObject.Find("Global").GetComponent<Global>().WaveCount;

        _text.text = "Wave: " + _currentWave + " of " + _totalWaves;
    }
}
