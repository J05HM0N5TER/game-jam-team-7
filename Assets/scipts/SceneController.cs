using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;

public class SceneController : MonoBehaviour
{
	[Header("Player scores")]
	List<int> playerMatchScores = new List<int>();

	[Header("Scene names")]
	// The name of the scene with the main menu
	public string mainMenuScene = "Main Menu";
	// The name of the scene with the main game
	public string gameScene;
	// The name of the scene with the win screen
	public string winScreen = "Win Screen";
	public XboxController previousLoss = XboxController.All;
	public int winAmount = 3;

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
			playerMatchScores.Add(0);
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
		if ((int)player > playerMatchScores.Count || (int)player < 0)
			return -1;
		return playerMatchScores[(int)player - 1];
	}

	public void SetPlayerScore(XboxController player, int newScore)
	{
		if ((int)player > playerMatchScores.Count || (int)player < 0)
			return;
		playerMatchScores[(int)player - 1] = newScore;
	}

	public void AddMatchLossToPlayer(XboxController player)
	{
		if ((int)player > playerMatchScores.Count || (int)player < 0)
			return;
		playerMatchScores[(int)player - 1]++;
		previousLoss = player;
		loadWinScreen();
	}
	public void ResetScores()
	{
		for (int i = 0; i < playerMatchScores.Count; i++)
		{
			playerMatchScores[i] = winAmount;
		}
	}
}
