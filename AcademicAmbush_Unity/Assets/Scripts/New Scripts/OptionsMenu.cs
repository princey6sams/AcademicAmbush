using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject previousMenu;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private TMP_Text volumeValue = null;
    void Start()
    {
        volumeSlider.value = 1;
        AudioListener.volume = volumeSlider.value;
        VolumeSlider();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            optionsMenu.SetActive(false);
            previousMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void VolumeSlider()
    {
        volumeValue.text = Mathf.RoundToInt(volumeSlider.value * 100).ToString() + '%';
        AudioListener.volume = volumeSlider.value;
    }


}