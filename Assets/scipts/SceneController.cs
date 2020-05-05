using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;

public class SceneController : MonoBehaviour
{
	[Header("Player scores")]
	List<int> playerScores = new List<int>();

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
		for (int i = 0; i < 4; i++)
		{
			playerScores.Add(0);
		}
		instance = this;
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

	public int GetPlayerScore(XboxController player)
	{
		if ((int)player > playerScores.Count || (int)player < 0)
			return -1;
		return playerScores[(int)player - 1];
	}

	public void SetPlayerScore(XboxController player, int newScore)
	{
		if ((int)player > playerScores.Count || (int)player < 0)
			return;
		playerScores[(int)player - 1] = newScore;
	}

	public void AddWinToPlayer(XboxController player)
	{
		if ((int)player > playerScores.Count || (int)player < 0)
			return;
		playerScores[(int)player - 1]--;
	}
}
