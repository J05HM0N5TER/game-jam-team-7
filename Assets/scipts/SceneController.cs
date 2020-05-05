using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
	[Header("Player scores")]
	public uint player1Points = 0;
	public uint player2Points = 0;

	[Header("Scene names")]
	// The name of the scene with the main menu
	public string mainMenuScene = "Main Menu";
	// The name of the scene with the main game
	public string gameScene;
	// The name of the scene with the win screen
	public string winScreen = "Win Screen";

	// The instance of this singleton
	private static SceneController instance = null;
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

	/// <summary>
	/// When the script loads set it to stay between scenes
	/// </summary>
	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	/// <summary>
	/// Check for if the player pressed the Esc button
	/// </summary>
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			quit();
	}

	/// <summary>
	/// Loads the main scene
	/// </summary>
	public void loadGame()
	{
		print("Game loading");
		SceneManager.LoadScene(gameScene);
	}

	/// <summary>
	/// Loads the main menu scene
	/// </summary>
	public void loadMenu()
	{
		print("Main menu loading");
		SceneManager.LoadScene(mainMenuScene);
	}

	/// <summary>
	/// Loads the end of game scene
	/// </summary>
	public void loadWinScreen()
	{
		print("Win Screen loading");
		SceneManager.LoadScene(winScreen);
	}

	/// <summary>
	/// Quits the game
	/// </summary>
	public void quit()
	{
		print("Quitting game");
		Application.Quit();
	}
}
