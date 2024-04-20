using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private TMP_Text _volumeText;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private string _volumeName;
    private float currentMixerVolume = 1f;

    private void OnEnable()
    {
        _volumeSlider.onValueChanged.AddListener((v) =>
        {
            UpdateValueOnChange(v);
        });
    }

    private void Start()
    {
        LoadValues();
    }
    public void UpdateValueOnChange(float value)
    {
        if (_mixer != null)
            _mixer.SetFloat(_volumeName, Mathf.Log10(value) * 20f);

        if (_volumeText != null && _volumeSlider != null)
            SetValue(value);
    }

    public void VolumeApply()
    {
        float volumeValue = _volumeSlider.value;
        PlayerPrefs.SetFloat(_volumeName, volumeValue);
    }

    public void LoadValues()
    {
        float localSettingsParametr = PlayerPrefs.GetFloat(_volumeName);
        if (localSettingsParametr == 0.0001)
            UpdateValueOnChange(currentMixerVolume);
        SetValue(localSettingsParametr);
    }

    private void SetValue(float value)
    {
        _volumeText.text = Mathf.Round(value * 100.0f).ToString() + "%";
        _volumeSlider.value = value;
    }
}
