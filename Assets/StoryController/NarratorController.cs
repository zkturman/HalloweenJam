using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class NarratorController : MonoBehaviour
{
    private BackgroundStoryUIController uiController;
    private int keyPressCounter = 0;
    // Update is called once per frame
    private void Awake()
    {
        uiController = FindObjectOfType<BackgroundStoryUIController>();
    }
    void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            if (keyPressCounter == 0)
            {
                uiController.EnableSkip();
            }
            if (keyPressCounter == 1)
            {
                SceneManager.LoadScene("MainGame");
            }
            keyPressCounter++;
        }
    }
}
