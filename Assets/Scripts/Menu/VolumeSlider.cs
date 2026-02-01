using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private float defaultVolume = 0.7f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        float saveVolume = PlayerPrefs.GetFloat("MasterVolume", defaultVolume);
        volumeSlider.value = saveVolume;

        volumeSlider.onValueChanged.AddListener(OnVolumeChange);

    }

    private void OnVolumeChange(float value)
    {
        //set volume for audio manager
        PlayerPrefs.SetFloat("MasterVolume", value);
    }
}
