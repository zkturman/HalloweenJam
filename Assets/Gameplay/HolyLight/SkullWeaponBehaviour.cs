using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullWeaponBehaviour : MonoBehaviour
{
    [SerializeField]
    private int maxCharges;
    private int remainingCharges;
    [SerializeField]
    private HolyLightBurstBehaviour lightBurst;
    [SerializeField]
    private float attackRadius;
    private PlayerStatUIHandler statsHandler;

    private void Awake()
    {
        statsHandler = FindObjectOfType<PlayerStatUIHandler>(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        remainingCharges = maxCharges;
        statsHandler.EnableSkull();
        statsHandler.SetNumberOfSkullCharges(maxCharges);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAttack()
    {
        if (remainingCharges > 0)
        {
            lightBurst.EmitLight();
            remainingCharges--;
            statsHandler.SetNumberOfSkullCharges(remainingCharges);
            affectMonsters();
        }
    }

    private void affectMonsters()
    {
        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, attackRadius, Physics.AllLayers, QueryTriggerInteraction.Collide);
        if (affectedObjects.Length > 0)
        {
            for (int i = 0; i < affectedObjects.Length; i++)
            {
                if (affectedObjects[i].tag == "Enemy")
                {
                    Debug.Log("Hit enemy with light!");
                }
            }
        }
    }

    public void Recharge()
    {
        remainingCharges = maxCharges;
        statsHandler.SetNumberOfSkullCharges(remainingCharges);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRadius);   
    }
}
