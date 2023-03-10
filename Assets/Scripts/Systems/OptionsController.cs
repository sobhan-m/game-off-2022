using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField] GameObject optionsMenu;
    [SerializeField] List<GameObject> uiElementsToDisable;
    [SerializeField] Slider volumeSlider;

    private void OnEnable()
    {
        CloseOptionsMenu();
        ApplySettingValues();
    }

    private void OnDisable()
    {
        CloseOptionsMenu();
    }

    public void OpenOptionsMenu()
    {
        if (optionsMenu)
        {
            optionsMenu.SetActive(true);
        }

        foreach (GameObject obj in uiElementsToDisable)
        {
            if (obj)
            {
                obj.SetActive(false);
            }
        }
    }

    public void CloseOptionsMenu()
    {
        if (optionsMenu)
        {
            optionsMenu.SetActive(false);
        }
        
        foreach (GameObject obj in uiElementsToDisable)
        {
            if (obj)
            {
                obj.SetActive(true);
            }
        }
    }

    public void FindVolumeMultiplier()
    {
        Settings.volumeMultiplier = volumeSlider.value;
    }

    private void ApplySettingValues()
    {
        volumeSlider.value = Settings.volumeMultiplier;
    }
}
