using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public delegate void GameHasEnded();
    public static event GameHasEnded gameHasEnded;

    private float _timer = 0f;
    private float _timeTillGameOVer = 1.9f;

    [SerializeField] private Score _score;

    private void OnTriggerStay2D(Collider2D collision)
    {
        _timer += Time.deltaTime;

        if(_timer > _timeTillGameOVer)
        {
            StartCoroutine(StratFade());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _timer = 0f;
    }

    private void IsItTheBestScore(int currentScore)
    {

        if (currentScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
        }
    }

    private IEnumerator StratFade()
    {
        gameHasEnded?.Invoke();

        yield return new WaitForSeconds(1.5f);

        Debug.Log("Game has restarted!");
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        IsItTheBestScore(_score.CurrentScore);
    }
}
