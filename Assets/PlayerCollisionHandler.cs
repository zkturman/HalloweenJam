using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
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
        if (collision.gameObject.tag == "LightRecharge")
        {
            Debug.Log("Recharging light!");
        }
        if (collision.gameObject.tag == "NetherShard")
        {

        }
    }
}
