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
	private PlayerInputActions inputs;
	private GameObject previousScene;

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
		if (dialogue == null)
		{
			SceneChangeManager sceneManager = FindObjectOfType<SceneChangeManager>();
			sceneManager.LoadNextScene();
			return;
		}

		dialogueText.text = dialogue.text;
		speakerText.text = dialogue.speaker;
		if (previousScene != null)
		{
			Destroy(previousScene);
		}
		previousScene = Instantiate(dialogue.characterImage, sceneHolder.transform.position, Quaternion.identity, sceneHolder.transform);
	}

	private void NextDialogue(InputAction.CallbackContext context)
	{
		dialogue = dialogue.nextDialogue;
		Populate();
	}
}
