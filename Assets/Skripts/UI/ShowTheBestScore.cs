using UnityEngine;
using TMPro;
using System.Text;
using System;

public class ShowTheBestScore : MonoBehaviour
{
    private int _score;
    private TextMeshProUGUI _scoreText;
    void Start()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();
        _score = PlayerPrefs.GetInt("HighScore", 0);
        _scoreText.text = "Your the best score: " + _score;
    }
}
