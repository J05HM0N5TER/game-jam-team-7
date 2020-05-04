using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SceneController : MonoBehaviour
{
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

	public string mainMenuScene;
	public string gameScene;
	public string winScreen;
	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}
	// Update is called once per frame
	void Start()
	{

	}
}
