using UnityEngine;
using StarterAssets;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField]
    private SkullWeaponBehaviour skullBehaviour;
    [SerializeField]
    private PlayerActionState controller;
    private JournalDisplayBehaviour journalDisplayBehaviour;
    private JournalBehaviour playerJournal;

    // Start is called before the first frame update
    void Start()
    {
        journalDisplayBehaviour = FindObjectOfType<JournalDisplayBehaviour>();
        playerJournal = FindObjectOfType<JournalBehaviour>(true);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "NetherShard")
        {
            Debug.Log("Collect Nether Shard!");
        }

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
                Destroy(other.gameObject);
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
                if (!playerJournal.IsPassengerFound(id))
                {
                    journalDisplayBehaviour.IndicateUpdatedEntry(id);
                    playerJournal.AddFoundPassenger(id);
                }
            }
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
