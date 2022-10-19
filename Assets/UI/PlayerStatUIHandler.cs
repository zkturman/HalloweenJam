using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatUIHandler : MonoBehaviour
{
    private MainGameUIController mainGameUIController;

    private void Awake()
    {
        mainGameUIController = transform.parent.GetComponentInChildren<MainGameUIController>(true);
    }

    public void AddHealth(int healthToAdd)
    {
        for (int i = 0; i < healthToAdd; i++)
        {
            mainGameUIController.AddHeart();
        }
    }

    public void SubtractHealth(int healthToSubtract)
    {
        for (int i = 0; i < healthToSubtract; i++)
        {
            mainGameUIController.RemoveHeart();
        }
    }

    public void EnableSkull()
    {
        mainGameUIController.EnablePlayerCounters();
    }

    public void SetNumberOfSkullCharges(int numberOfCharges)
    {
        string chargeText = string.Format("{0}", numberOfCharges);
        mainGameUIController.SetChargeCount(chargeText);
    }

    public void SetNumberOfShards(int collectedShards)
    {
        string shardText = string.Format("{0}", collectedShards);
        mainGameUIController.SetShardCount(shardText);
    }
}
