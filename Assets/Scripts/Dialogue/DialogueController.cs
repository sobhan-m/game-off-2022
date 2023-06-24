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
	private PlayerInputActions inputs;

	private void Awake()
	{
		Populate();

		inputs = new PlayerInputActions();
		inputs.Dialogue.Progress.performed += NextDialogue;
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
		dialogueText.text = dialogue.text;
		speakerText.text = dialogue.speaker;
	}

	private void NextDialogue(InputAction.CallbackContext context)
	{
		dialogue = dialogue.nextDialogue;
		Populate();
	}
}
