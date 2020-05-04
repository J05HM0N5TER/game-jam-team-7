using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public SceneController controller;
	public Button playButton = null;
	public Button quitButton = null;
	// Start is called before the first frame update
	void Start()
	{
		controller = SceneController.Instance;
		// Set what the buttons do
		playButton.onClick.AddListener(controller.loadGame);
		quitButton.onClick.AddListener(controller.quit);
	}
}
