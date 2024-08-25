using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{

	static private int savedSceneIndex = 0;
	public void LoadMainMenu()
	{
		SceneManager.LoadScene("0 - Main_Menu");
	}

	public void LoadGameOver()
	{
		SceneManager.LoadScene("X - Game_Over");
	}

	public void LoadNextScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void LoadSavedScene()
	{
		SceneManager.LoadScene(savedSceneIndex);
	}

	public void SaveCurrentScene()
	{
		savedSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
}
