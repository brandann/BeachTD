using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveDisplay : MonoBehaviour {

    private int _totalWaves;
    private int _currentWave;
    private Text _text;

	// Use this for initialization
	void Awake () {

        _currentWave = 0;
        _totalWaves = GameObject.Find("Global").GetComponent<Global>().WaveCount;
        _text = gameObject.GetComponent<Text>();
	}    

    void OnEnable()
    {
        EnemyManager.OnWaveStarted += HandleWaveStart;
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
        if (_totalWaves == 0)
            _totalWaves = GameObject.Find("Global").GetComponent<Global>().WaveCount;

        _text.text = "Wave: " + _currentWave + " of " + _totalWaves;
    }
}
