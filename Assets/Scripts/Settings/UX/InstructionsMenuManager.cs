using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsMenuManager : MonoBehaviour
{
	[SerializeField] GameObject mainMenu;
	[SerializeField] GameObject instructionsMenu;

	public void OpenInstructionsMenu()
	{
		mainMenu.SetActive(false);
		instructionsMenu.SetActive(true);
	}

	public void CloseInstructionsMenu()
	{
		mainMenu.SetActive(true);
		instructionsMenu.SetActive(false);
	}

}
