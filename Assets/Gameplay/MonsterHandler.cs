using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHandler : MonoBehaviour
{
    public static int NumberOfPassengers = 12;
    [SerializeField]
    private string[] PossiblePlayerDescriptions = new string[NumberOfPassengers];
    [SerializeField]
    private string[] PossibleUnomosDescriptions = new string[NumberOfPassengers];
    [SerializeField]
    private Color[] PossibleColorIndicators = new Color[NumberOfPassengers];

    private Dictionary<int, JournalSubject> allPassengers = new Dictionary<int, JournalSubject>();
    
    public static int NumberOfShards = 4;
    private int[] shardPasssengers = new int[NumberOfShards];

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < NumberOfPassengers; i++)
        {
            allPassengers.Add(i, null);
        }
        List<JournalSubject> foundPassengers = findAllPassengers();
        assignPassengerIds(foundPassengers);
        assignPassengerColors();
        List<int> passengerList = new List<int>(allPassengers.Keys);
        for (int i = 0; i < NumberOfShards; i++)
        {
            int diceRoll = Random.Range(0, passengerList.Count);
            shardPasssengers[i] = passengerList[diceRoll];
            passengerList.RemoveAt(diceRoll);
        }
    }

    private List<JournalSubject> findAllPassengers()
    {
        List<JournalSubject> monsters = new List<JournalSubject>(FindObjectsOfType<JournalSubject>());
        return monsters;
    }

    private void assignPassengerIds(List<JournalSubject> monsters)
    {
        List<int> possibleIds = new List<int>();
        for (int i = 0; i < monsters.Count; i++)
        {
            possibleIds.Add(i);
        }
        for (int i = 0; i < monsters.Count; i++)
        {
            int diceRoll = Random.Range(0, possibleIds.Count);
            allPassengers[diceRoll] = monsters[i];
            monsters[i].ID = possibleIds[diceRoll];
            possibleIds.RemoveAt(diceRoll);
        }
    }

    private void assignPassengerColors()
    {
        for (int i = 0; i < allPassengers.Count; i++)
        {
            JournalSubject passenger = allPassengers[i];
            if (passenger != null)
            {
                allPassengers[i].ColorTag = PossibleColorIndicators[i];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetDescriptionFromMonsterId(int monsterId)
    {
        return PossiblePlayerDescriptions[monsterId]; 
    }

    public string GetHintFromIndex(int hintNumber)
    {
        int monsterId = shardPasssengers[hintNumber];
        string hintValue = PossibleUnomosDescriptions[monsterId];
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
