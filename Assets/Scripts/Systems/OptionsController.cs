using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField] GameObject optionsMenu;
    [SerializeField] List<GameObject> uiElementsToDisable;
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider combatSpeedSlider;

    private PauseController pauseController;

    private void Awake()
    {
        pauseController = gameObject.GetComponent<PauseController>();
        if (!pauseController)
        {
            Debug.Log("OptionsController.Awake(): No PauseController found on this object!");
        }
    }

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

        if (pauseController)
        {
            pauseController.isActive = false;
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

        if (pauseController)
        {
            pauseController.isActive = true;
        }
    }

    public void FindVolumeMultiplier()
    {
        Settings.volumeMultiplier = volumeSlider.value;
        Debug.Log("FindVolumeMultiplier(): " + Settings.volumeMultiplier);
    }

    public void FindCombatSpeedMultiplier()
    {
        Settings.combatSpeedMultiplier = combatSpeedSlider.value;
    }

    private void ApplySettingValues()
    {
        volumeSlider.value = Settings.volumeMultiplier;
        volumeSlider.onValueChanged.AddListener((float arg) => FindVolumeMultiplier());

        combatSpeedSlider.value = Settings.combatSpeedMultiplier;
        combatSpeedSlider.onValueChanged.AddListener((float arg) => FindCombatSpeedMultiplier());
    }

}
