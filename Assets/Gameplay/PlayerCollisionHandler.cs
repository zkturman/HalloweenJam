using UnityEngine;
using StarterAssets;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField]
    private SkullWeaponBehaviour skullBehaviour;
    [SerializeField]
    private PlayerActionState controller;

    // Start is called before the first frame update
    void Start()
    {
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
            Debug.Log("Should enable interaction.");
            controller.CanInteract = true;
            other.gameObject.GetComponent<DialogueEmitter>().EmitText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable")
        {
            Debug.Log("Should disable interaction");
            controller.CanInteract = false;
        }
    }
}
