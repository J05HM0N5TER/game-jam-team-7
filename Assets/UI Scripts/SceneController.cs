using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
	private static SceneController instance = null;
	[Header("Player scores")]
	public uint player1Points = 0;
	public uint player2Points = 0;


	public static SceneController Instance
	{
		get
		{
			if (instance = null)
			{
				instance = new SceneController();
			}
			return instance;
		}
	}
	[Header("Scene names")]
	public string mainMenuScene = "Main Menu";
	public string gameScene;
	public string winScreen = "Win Screen";
	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
	}

	public void loadGame()
	{
		print("Game loading");
		SceneManager.LoadScene(gameScene);
	}

	public void loadMenu()
	{
		print("Main menu loading");
		SceneManager.LoadScene(mainMenuScene);
	}
	public void loadWinScreen()
	{
		print("Win Screen loading");
		SceneManager.LoadScene(winScreen);
	}

	public void quit()
	{
		print("Quitting game");
		Application.Quit();
	}
}
