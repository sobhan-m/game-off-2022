using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
	public void LoadMainMenu()
	{
		SceneManager.LoadScene("Main_Menu");
	}

	public void LoadGameOver()
	{
		SceneManager.LoadScene("Game_Over");
	}

	public void LoadNextScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
