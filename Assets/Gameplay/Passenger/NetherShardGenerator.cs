using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetherShardGenerator : MonoBehaviour
{
    private GameObject netherShardPrefab;
    private bool hasDroppedNetherShard = false;
    [SerializeField]
    private float propulsionForce = 15f;
    [SerializeField]
    private float heightOffset = 1f;
    private MonsterHandler monsterHandler;

    private void Awake()
    {
        netherShardPrefab = Resources.Load("NetherShard") as GameObject;
        monsterHandler = FindObjectOfType<MonsterHandler>();
    }

    public void GenerateNetherShard(float ejectionAngle)
    {
        if (canDropShard())
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + heightOffset, transform.position.z);
            GameObject netherShard = Instantiate(netherShardPrefab, spawnPosition, Quaternion.identity);
            Rigidbody shardRigidBody = netherShard.GetComponent<Rigidbody>();
            Vector3 ejectDirection = new Vector3(Mathf.Sin(ejectionAngle)
                , 0f
                , Mathf.Cos(ejectionAngle));
            shardRigidBody.AddForce(ejectDirection * propulsionForce);
            hasDroppedNetherShard = true;
        }
    }
     
    private bool canDropShard()
    {
        bool canDrop = false;
        JournalSubject journalSubject = GetComponent<JournalSubject>();
        if (journalSubject != null)
        {
            int monsterID = GetComponent<JournalSubject>().ID;
            bool hasShard = monsterHandler.IsShardMonster(monsterID);
            canDrop = !hasDroppedNetherShard && hasShard;
        }
        return canDrop;
    }
}
