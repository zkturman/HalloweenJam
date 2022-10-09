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

    // Start is called before the first frame update
    void Start()
    {
        remainingCharges = maxCharges;
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
        }
    }

    private void affectMonsters()
    {
        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, attackRadius, Physics.AllLayers, QueryTriggerInteraction.Collide);
        if (affectedObjects.Length > 0)
        {

        }
    }

    public void Recharge()
    {
        remainingCharges = maxCharges;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRadius);   
    }
}
