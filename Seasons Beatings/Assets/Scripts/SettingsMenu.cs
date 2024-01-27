using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    private bool SettingsOpen;

    private void Start()
    {
        SettingsOpen = false;
    }

    public void SetVolume(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("volume", volume);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!SettingsOpen)
            {
                OpenSettings();
            }
            else
            {
                CloseSettings();
            }
        }
    }

    private void OpenSettings()
    {
        gameObject.SetActive(true);
        SettingsOpen = true;
        Debug.Log("Open");
    }

    private void CloseSettings()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        SettingsOpen = false;
    }
}
