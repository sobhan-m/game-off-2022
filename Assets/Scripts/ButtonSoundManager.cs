using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSoundManager : MonoBehaviour
{
    [Tooltip("This field is not currently in use.")]
    [SerializeField] AudioClip mouseOverSound;
    [SerializeField] AudioClip clickSound;

    private List<Button> buttons;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        buttons = new List<Button>(Resources.FindObjectsOfTypeAll(typeof(Button)) as Button[]);

        foreach (Button button in buttons)
        {
            button.onClick.AddListener(PlayClickSound);
        }
    }

    public void PlayClickSound()
    {
        AudioSource.PlayClipAtPoint(clickSound, mainCamera.transform.position);
    }
}
