using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseController : MonoBehaviour
{
	public bool isActive { set; get; }
	[SerializeField] GameObject pauseMenu;

	private static bool isPaused;

	private InputAction pause;
	private PlayerInputActions playerInputActions;

	private void Awake()
	{
		playerInputActions = new PlayerInputActions();
	}

	private void Update()
	{
		if (pause.triggered)
		{
			if (isPaused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}
	}

	private void Start()
	{
		Resume();
	}

	private void OnEnable()
	{
		pause = playerInputActions.Combat.Pause;
		pause.Enable();

		Resume();
	}

	private void OnDisable()
	{
		pause.Disable();

		Resume();
	}

	public void Pause()
	{
		if (!isActive)
		{
			return;
		}

		isPaused = true;
		Time.timeScale = 0;
		if (pauseMenu)
		{
			pauseMenu.SetActive(true);
		}
	}

	public void Resume()
	{
		if (!isActive)
		{
			return;
		}

		isPaused = false;
		Time.timeScale = 1;

		if (pauseMenu)
		{
			pauseMenu.SetActive(false);
		}
	}

	public static bool IsPaused()
	{
		return isPaused;
	}

}
