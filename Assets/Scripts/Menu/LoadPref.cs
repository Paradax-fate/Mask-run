using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LoadPref : MonoBehaviour
{
    [SerializeField] private float defaultVolume = 0.7f;

    void Start()
    {
        StartCoroutine(languageLoad());
        volumeLoad();

    }


    IEnumerator languageLoad()
    {

        Debug.Log("start reading save language");
        yield return LocalizationSettings.InitializationOperation;

        string saveLocale = PlayerPrefs.GetString("SelectedLocale");

        for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; ++i)
        {
            Debug.Log("REading aavailable langagues");
            var locale = LocalizationSettings.AvailableLocales.Locales[i];
            if (locale.LocaleName == saveLocale)
            {
                Debug.Log("lanagauge selected: " + saveLocale);
                LocalizationSettings.SelectedLocale = locale;
            }
        }
    }

    private void volumeLoad()
    {
        float saveVolume = PlayerPrefs.GetFloat("MasterVolume", defaultVolume);

        //set volume in audio manager
    }
}
