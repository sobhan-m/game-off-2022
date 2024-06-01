using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField]
    Slider slider;
    [SerializeField]
    public int seconds;
    public Meter meter;

    private void Start()
    {
        meter = new Meter(0, seconds);
        slider.maxValue = meter.maxValue;
        slider.minValue = meter.minValue;
        slider.value = meter.minValue;
    }

    private void Update()
    {
        meter.FillMeter(Time.deltaTime);
        slider.value = meter.currentValue;
        if (meter.IsFull())
        {
            GoToNextScene();
        }
    }

    private void GoToNextScene()
    {
        SceneChangeManager sceneChangeManager = FindObjectOfType<SceneChangeManager>();
        sceneChangeManager.LoadNextScene();
    }
}
