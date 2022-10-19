using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardCollectionHandler : MonoBehaviour
{
    private int numberOfShards = 0;
    private PlayerStatUIHandler statUIHandler;

    private void Awake()
    {
        statUIHandler = FindObjectOfType<PlayerStatUIHandler>(true);
    }

    public void IncrementShards()
    {
        numberOfShards++;
        statUIHandler.SetNumberOfShards(numberOfShards);
    }
}
