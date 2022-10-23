using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject playerHud;
    private PauseMenuUIController menuUI;

    private void Awake()
    {
        menuUI = GetComponentInChildren<PauseMenuUIController>(true);
    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    public bool ShouldCloseMenu()
    {
        return menuUI.HasUserPressedContinue();
    }
}
