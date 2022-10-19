using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalSubject : MonoBehaviour
{
    [Range(0, 11)]
    [Tooltip("The ID is used to identify the monster and journal entry. Must be unique.")]
    public int ID;

    [Tooltip("The Colour is used to help identify the monster with the player's journal. Must be unique.")]
    public Color ColorTag;

    private RingColourHandler colourHandler;

    private void Awake()
    {
        colourHandler = GetComponentInChildren<RingColourHandler>(true);
    }
    public void ActivateColorIdentifier()
    {
        colourHandler.UpdateRingColor(ColorTag);
        colourHandler.gameObject.SetActive(true);
    }
}
