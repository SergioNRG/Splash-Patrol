using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITxtUpdate : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _waveNum;
    [SerializeField] private TextMeshProUGUI _scoreNum;

    void Start()
    {
        EnemySpawner.instance.OnWaveChanged += UpdateWaveText;
        ScoreManager.Instance.OnScoreChanged += UpdateScoreText;
    }

    private void OnDisable()
    {
        EnemySpawner.instance.OnWaveChanged -= UpdateWaveText;
        ScoreManager.Instance.OnScoreChanged -= UpdateScoreText;
    }

    private void UpdateScoreText()
    {
        _scoreNum.text = ScoreManager.Instance.GetScore().ToString();
    }
    private void UpdateWaveText()
    {
        _waveNum.text = EnemySpawner.instance.WaveCount.ToString();
    }

}
