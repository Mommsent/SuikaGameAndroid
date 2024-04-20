using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public int CurrentScore {  get; private set; }
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Awake()
    {
        _scoreText.text = CurrentScore.ToString("0");
        ColliderInformer.wasCollided += Increase;
    }

    private void OnDisable()
    {
        ColliderInformer.wasCollided += Increase;
    }

    private void Increase(int amount)
    {
        CurrentScore += amount;
        _scoreText.text = CurrentScore.ToString();
    }
}
