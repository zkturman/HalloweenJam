using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField]
    private SkullWeaponBehaviour skullBehaviour;
    [SerializeField]
    private float interactionMaxDistance;
    private LayerMask interactionLayer;
    // Start is called before the first frame update
    void Start()
    {
        interactionLayer = LayerMask.GetMask("Interactable");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DetectInteractables()
    {
        if (Physics.Raycast(transform.position, transform.forward, interactionMaxDistance, interactionLayer))
        {

        }

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
    }
}
