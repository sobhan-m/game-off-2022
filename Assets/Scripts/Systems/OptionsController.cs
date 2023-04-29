using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsController : MonoBehaviour
{
    [SerializeField] GameObject optionsMenu;
    [SerializeField] List<GameObject> uiElementsToDisable;

    [Header("Speed Settings")]
    [SerializeField] Slider combatSpeedSlider;
    [SerializeField] TMP_InputField combatSpeedInput;

    [Header("Volume Settings")]
    [SerializeField] Slider volumeSlider;
    [SerializeField] TMP_InputField volumeInput;

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

    public void SyncVolume(float newVolume)
    {
        float clampedValue = Mathf.Clamp(newVolume, volumeSlider.minValue, volumeSlider.maxValue);

        volumeSlider.value = clampedValue;
        Settings.volumeMultiplier = clampedValue;
        volumeInput.text = string.Format("{0:0.0}", clampedValue);
    }

    public void SyncCombatSpeed(float newSpeed)
    {
        float clampedValue = Mathf.Clamp(newSpeed, combatSpeedSlider.minValue, combatSpeedSlider.maxValue);

        combatSpeedSlider.value = clampedValue;
        Settings.combatSpeedMultiplier = clampedValue;
        combatSpeedInput.text = string.Format("{0:0.0}", clampedValue);
    }

    private void ApplySettingValues()
    {
        SyncVolume(Settings.volumeMultiplier);
        volumeSlider.onValueChanged.AddListener((float val) => SyncVolume(val));
        volumeInput.onEndEdit.AddListener((string val) => SyncVolume(float.Parse(val)));

        SyncCombatSpeed(Settings.combatSpeedMultiplier);
        combatSpeedSlider.onValueChanged.AddListener((float val) => SyncCombatSpeed(val));
        combatSpeedInput.onEndEdit.AddListener((string val) => SyncCombatSpeed(float.Parse(val)));
    }

}
