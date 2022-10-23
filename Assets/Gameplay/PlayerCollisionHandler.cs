using UnityEngine;
using StarterAssets;

public class PlayerCollisionHandler : MonoBehaviour
{
    private SkullWeaponBehaviour skullBehaviour;
    private PlayerActionState controller;
    private JournalDisplayBehaviour journalDisplayBehaviour;
    private JournalBehaviour playerJournal;
    private GameStateController gameStateController;
    private ShardCollectionHandler shardCollectionHandler;
    // Start is called before the first frame update
    void Start()
    {
        journalDisplayBehaviour = FindObjectOfType<JournalDisplayBehaviour>();
        playerJournal = FindObjectOfType<JournalBehaviour>(true);
        gameStateController = FindObjectOfType<GameStateController>();
        controller = GetComponent<PlayerActionState>();
        skullBehaviour = GetComponentInChildren<SkullWeaponBehaviour>(true);
        shardCollectionHandler = GetComponent<ShardCollectionHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LightRecharge")
        {
            if (skullBehaviour.gameObject.activeSelf)
            {
                skullBehaviour.Recharge();
            }
        }
        if (other.tag == "EffulgentSkull")
        {
            if (!skullBehaviour.enabled)
            {
                skullBehaviour.gameObject.SetActive(true);
                skullBehaviour.enabled = true;
                other.GetComponent<SkullOverworldBehaviour>().PickupSkull();
                gameStateController.SetRetrievedSkullState();
            }
        }
        if (other.tag == "Interactable")
        {
            controller.CanInteract = true;
            other.gameObject.GetComponent<DialogueEmitter>().EmitText();
        }
        if (other.tag == "JournalSubject")
        {
            JournalSubject subject = other.GetComponent<JournalSubject>();
            if (subject != null)
            {
                int id = subject.ID;
                Color identifier = subject.ColorTag;
                if (!playerJournal.IsPassengerFound(id))
                {
                    journalDisplayBehaviour.IndicateUpdatedEntry(id);
                    playerJournal.AddFoundPassenger(id);
                    playerJournal.AddPassengerColorTag(id, identifier);
                    subject.ActivateColorIdentifier();
                }
            }
        }
        if (other.tag == "NetherShard")
        {
            shardCollectionHandler.IncrementShards();
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable")
        {
            controller.CanInteract = false;
        }
    }
}
