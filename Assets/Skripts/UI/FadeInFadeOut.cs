using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInFadeOut : MonoBehaviour
{
    [SerializeField] private Image _panal;
    [SerializeField] private float _timeToFade;

    [SerializeField] private UserInput input;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += FadeOut;
        GameOver.gameHasEnded += FadeIn;
    }

    private void FadeIn()
    {
        StartCoroutine(Fade());
    }

    private void FadeOut(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(Fade(1f,0f));
    }

    private IEnumerator Fade(float startValue = 0f, float endValue = 1f)
    {
        input.enabled = false;

        _panal.gameObject.SetActive(true);
        Color startColor = _panal.color;
        startColor.a = startValue;
        _panal.color = startColor;

        float elapsedTime = startValue;
        while (elapsedTime < _timeToFade)
        {
            elapsedTime += Time.deltaTime;

            float newAlpha = Mathf.Lerp(startValue, endValue, (elapsedTime / _timeToFade));
            startColor.a = newAlpha;
            _panal.color = startColor;

            yield return null;
        }

        _panal.gameObject.SetActive(false);
        input.enabled = true;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= FadeOut;
        GameOver.gameHasEnded -= FadeIn;
    }
}
