using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullWeaponBehaviour : MonoBehaviour
{
    [SerializeField]
    private int maxCharges;
    private int remainingCharges;
    private bool isAttacking = false;
    [SerializeField]
    private HolyLightBurstBehaviour lightBurst;
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

    private bool canStartAttack()
    {
        return remainingCharges > 0 && !isAttacking;
    }

    //private IEnumerator attackRoutine()
    //{
    //    isAttacking = true;
    //    remainingCharges--;
    //    yield return new WaitForSeconds(cooldownInSeconds);
    //    isAttacking = false;
    //}

    public void Recharge()
    {
        remainingCharges = maxCharges;
    }
}
