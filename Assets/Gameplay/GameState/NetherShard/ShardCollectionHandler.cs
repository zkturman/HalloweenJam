using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardCollectionHandler : MonoBehaviour
{
    private int numberOfShards = 0;
    private PlayerStatUIHandler statUIHandler;
    private GameStateController gameStateController;
    [SerializeField]
    private AudioSource collectionSoundPlayer;
    [SerializeField]
    private AudioClip collectionSound;

    private void Awake()
    {
        statUIHandler = FindObjectOfType<PlayerStatUIHandler>(true);
        gameStateController = FindObjectOfType<GameStateController>();
    }

    public void IncrementShards()
    {
        numberOfShards++;
        statUIHandler.SetNumberOfShards(numberOfShards);
        if (numberOfShards == MonsterHandler.NumberOfShards)
        {
            gameStateController.SetHasAllShardsState();
        }
        collectionSoundPlayer.PlayOneShot(collectionSound);
    }
}
