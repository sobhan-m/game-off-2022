using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI speakerText;
    [SerializeField] Dialogue dialogue;
    [SerializeField] GameObject sceneHolder;
    [SerializeField] GameObject speakerArea;
    [SerializeField] float secondsBetweenCharacters;
    private PlayerInputActions inputs;
    private GameObject previousScene;
    private bool isPrinting;
    private Coroutine printingProcess;

    private void Awake()
    {
        inputs = new PlayerInputActions();
        inputs.Dialogue.Progress.performed += NextDialogue;
        isPrinting = false;

        Populate();
    }

    private void OnEnable()
    {
        inputs.Dialogue.Progress.Enable();
    }

    private void OnDisable()
    {
        inputs.Dialogue.Progress.Disable();
    }

    private void Populate()
    {
        if (dialogue == null)
        {
            LoadNextScene();
            return;
        }

        UpdateSceneText();
        ActivateSpeakerAreaIfNeeded();
        UpdateSceneImage();
    }

    private void UpdateSceneText()
    {
        speakerText.text = dialogue.speaker;
        printingProcess = StartCoroutine(UpdateText());
    }

    private IEnumerator UpdateText()
    {
        isPrinting = true;

        for (int i = 1; i <= dialogue.text.Length; ++i)
        {
            dialogueText.text = dialogue.text.Substring(0, i);
            yield return new WaitForSeconds(secondsBetweenCharacters);
        }

        isPrinting = false;
    }

    private void ActivateSpeakerAreaIfNeeded()
    {
        bool hasSpeaker = speakerText.text != "" && speakerText.text.ToLower() != "bard";
        speakerArea.SetActive(hasSpeaker);
    }

    private void UpdateSceneImage()
    {
        DeleteOldSceneImage();
        CreateNewSceneImage();
    }

    private void CreateNewSceneImage()
    {
        if (dialogue.characterImage != null)
        {
            previousScene = Instantiate(dialogue.characterImage, sceneHolder.transform.position, Quaternion.identity, sceneHolder.transform);
        }
    }

    private void DeleteOldSceneImage()
    {
        if (previousScene != null)
        {
            Destroy(previousScene);
        }
    }

    private void LoadNextScene()
    {
        SceneChangeManager sceneManager = FindObjectOfType<SceneChangeManager>();
        sceneManager.LoadNextScene();
    }

    private void NextDialogue(InputAction.CallbackContext context)
    {
        if (isPrinting)
        {
            CancelPrinting();
            isPrinting = false;
        }
        else
        {
            dialogue = dialogue.nextDialogue;
            Populate();
        }
    }

    private void CancelPrinting()
    {
        StopCoroutine(printingProcess);
        dialogueText.text = dialogue.text;
    }
}
