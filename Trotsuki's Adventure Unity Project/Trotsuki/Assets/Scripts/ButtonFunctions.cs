using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour
{
	public Image pauseMenu;
	public GameObject choicesManager;
	public void OpenMainMenu()
	{
		SceneManager.LoadScene("Main Menu");
	}
	public void RunGame()
	{
		SceneManager.LoadScene("Scene 1");
	}
	public void QuitGame()
	{
		Application.Quit();
	}
	public void Resume()
	{
		Time.timeScale = 1;
		choicesManager.SetActive(true);
		pauseMenu.gameObject.SetActive(false);
	}
	public void PauseGame()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!pauseMenu.gameObject.activeSelf)
			{
				Time.timeScale = 0;
				pauseMenu.gameObject.SetActive(true);
				choicesManager.SetActive(false);
			}
			else
			{
				Resume();
			}
		}


	}
	// Use this for initialization
	private void Start()
	{

	}

	// Update is called once per frame
	private void Update()
	{
		if (SceneManager.GetActiveScene().name == "Scene 1")
		{
			PauseGame();
		}

	}
}
