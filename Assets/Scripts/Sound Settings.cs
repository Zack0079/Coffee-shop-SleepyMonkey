using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] Slider soundSlider;
    [SerializeField] AudioMixer masterMixer;

    // Start is called before the first frame update
    private void Start()
    {
        SetVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 100));
    }

    public void SetVolume(float _value)
    {
        if (_value < 1)
        {
            _value = .001f;   // Set the minimum volume to 0.1%
        }

        RefreshSlider(_value);
        PlayerPrefs.SetFloat("SavedMasterVolume", _value);
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(_value / 100) * 20f);
    }

    public void RefreshSlider(float _value)
    {
        soundSlider.value = _value;
    }

    public void SetVolumeSlider(float _value)
    {
        SetVolume(soundSlider.value);
    }
}