using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] List<Button> responseButtons;
    [SerializeField] Dialogue dialogue;

    private void Start()
    {
        PopulateUI();
    }

    private void PopulateUI()
    {
        dialogueText.text = dialogue.dialogueText;

        Debug.Log(dialogue.responseTexts.Count);

        int maxResponseIndex = -1;
        for (int i = 0; i < dialogue.responseTexts.Count; ++i)
        {
            if (!responseButtons[i])
            {
                throw new MissingReferenceException("Not enough buttons are being referenced.");
            }
            TextMeshProUGUI buttonText = responseButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (!buttonText)
            {
                throw new MissingReferenceException("No TextMeshPro attached to this object.");
            }

            responseButtons[i].gameObject.SetActive(true);
            buttonText.text = dialogue.responseTexts[i];
            Dialogue responseDialogue = dialogue.responseDialogues[i];
            responseButtons[i].onClick.AddListener(delegate { ProgressDialogue(responseDialogue); });

            maxResponseIndex = i;
        }

        // Disabling unused buttons.
        for (int i = maxResponseIndex + 1; i < responseButtons.Count; ++i)
        {
            responseButtons[i].gameObject.SetActive(false);
        }
    }

    public void ProgressDialogue(Dialogue futureDialogue)
    {
        dialogue = futureDialogue;
        PopulateUI();
    }
}
