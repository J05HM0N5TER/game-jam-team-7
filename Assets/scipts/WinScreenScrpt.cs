using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class WinScreenScrpt : MonoBehaviour
{
    public GameObject DemonWinUI;
    public GameObject HumanWinUI;

    private SceneController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<SceneController>();
        if (controller.previousLoss == XboxController.First)
        {
            HumanWinUI.SetActive(true);
            DemonWinUI.SetActive(false);
        }
        else
        {
            HumanWinUI.SetActive(false);
            DemonWinUI.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
