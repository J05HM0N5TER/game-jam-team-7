using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] XboxController controller = XboxController.All;
    private bool ispaused = false;
    private Canvas pauseCanvas;

    [Header("Button Pressed Settings")]
    AudioSource Audio;
    public AudioClip ButtonPressedSound;
    public float ButtonPressedVolume = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        pauseCanvas = gameObject.GetComponent<Canvas>();
        pauseCanvas.enabled = false;
        Audio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(XCI.GetButtonDown(XboxButton.Start, controller) && !ispaused)
        {
            Audio.PlayOneShot(ButtonPressedSound, ButtonPressedVolume);
            ispaused = true;
            pauseCanvas.enabled = true;
            Time.timeScale = 0;
            
            
        }
        if (XCI.GetButtonDown(XboxButton.B, controller) && ispaused)
        {
            Audio.PlayOneShot(ButtonPressedSound, ButtonPressedVolume);
            Time.timeScale = 1;
            pauseCanvas.enabled = false;
            ispaused = false;
            
        }
        if (XCI.GetButtonDown(XboxButton.Back, controller) && ispaused)
        {
            Audio.PlayOneShot(ButtonPressedSound, ButtonPressedVolume);
            Time.timeScale = 1;
            ispaused = false;
            SceneController sceneController = FindObjectOfType<SceneController>();
            sceneController.loadMenu();

        }
    }
}
