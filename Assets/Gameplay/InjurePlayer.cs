using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjurePlayer : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<HealthHandler>().TakeHealthDamage();
        }
    }
}
