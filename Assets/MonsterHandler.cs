using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHandler : MonoBehaviour
{
    public static int NumberOfPassengers = 12;
    [SerializeField]
    private Vector3[] PossibleMonsterLocations = new Vector3[NumberOfPassengers];
    [SerializeField]
    private string[] playerDescriptions = new string[NumberOfPassengers];
    [SerializeField]
    private string[] unomosDescriptions = new string[NumberOfPassengers];

    private Dictionary<int, GameObject> allPassengers = new Dictionary<int, GameObject>();
    
    public static int NumberOfShards = 4;
    private int[] shardPasssengers = new int[NumberOfShards];

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < NumberOfPassengers; i++)
        {
            allPassengers.Add(i, null);
        }
        List<int> passengerList = new List<int>(allPassengers.Keys);
        for (int i = 0; i < NumberOfShards; i++)
        {
            int diceRoll = Random.Range(0, passengerList.Count);
            shardPasssengers[i] = passengerList[diceRoll];
            passengerList.RemoveAt(diceRoll);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetDescriptionFromMonsterId(int monsterId)
    {
        return playerDescriptions[monsterId]; 
    }

    public string GetHintFromIndex(int hintNumber)
    {
        int monsterId = shardPasssengers[hintNumber];
        string hintValue = unomosDescriptions[monsterId];
        return hintValue;
    }

    public bool IsShardMonster(int monsterId)
    {
        bool hasShard = false;
        for (int i = 0; i < shardPasssengers.Length; i++)
        {
            if (shardPasssengers[i] == monsterId)
            {
                hasShard = true;
            }
        }
        return hasShard;
    }
}
