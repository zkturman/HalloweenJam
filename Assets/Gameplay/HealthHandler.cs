using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    [SerializeField]
    private int TotalHealth;
    private int currentHealth;
    private bool takingDamage = false;
    [SerializeField]
    private float secondsOfInvulnerability;
    private PlayerStatUIHandler healthHandler;
    private GameStateController gameStateController;

    private void Awake()
    {
        healthHandler = FindObjectOfType<PlayerStatUIHandler>(true);
        gameStateController = FindObjectOfType<GameStateController>();
        currentHealth = TotalHealth;
    }

    private void Start()
    {
        healthHandler.AddHealth(TotalHealth);
    }

    public void TakeHealthDamage()
    {
        Debug.Log("HealthHander.TakeHealthDamage() has been called...");

        if (!takingDamage)
        {
            takingDamage = true;
            StartCoroutine(reduceHealthRoutine());
        }
    }

    private IEnumerator reduceHealthRoutine()
    {
        currentHealth--;
        if (currentHealth == 0)
        {
            gameStateController.SetGameOverState();
        }
        healthHandler.SubtractHealth(1);
        yield return new WaitForSeconds(secondsOfInvulnerability);
        takingDamage = false;
    }

}
