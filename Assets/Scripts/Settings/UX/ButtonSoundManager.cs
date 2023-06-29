using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSoundManager : MonoBehaviour
{
	[Tooltip("This field is not currently in use.")]
	[SerializeField] AudioClip[] mouseOverSound;
	[SerializeField] AudioClip[] clickSound;

	private List<Button> buttons;
	private Camera mainCamera;

	private void Start()
	{
		mainCamera = Camera.main;

		buttons = new List<Button>(Resources.FindObjectsOfTypeAll(typeof(Button)) as Button[]);

		foreach (Button button in buttons)
		{
			button.onClick.AddListener(PlayClickSound);

			// Adding mouse over event.
			EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();
			EventTrigger.Entry newEvent = new EventTrigger.Entry();
			newEvent.eventID = UnityEngine.EventSystems.EventTriggerType.PointerEnter;
			newEvent.callback.AddListener(PlayMouseOverSound);
			trigger.triggers.Add(newEvent);
		}
	}


	public void PlayClickSound()
	{
		int i = Random.Range(0, clickSound.Length);
		AudioSource.PlayClipAtPoint(clickSound[i], mainCamera.transform.position);
	}

	public void PlayMouseOverSound(BaseEventData arg)
	{
		int i = Random.Range(0, mouseOverSound.Length);
		AudioSource.PlayClipAtPoint(mouseOverSound[i], mainCamera.transform.position);
	}
}
