using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardRotation : MonoBehaviour
{
    private Rigidbody shardRigidBody;
    [SerializeField]
    private float rotationDegPerSecond = 45;
    // Start is called before the first frame update
    void Start()
    {
        shardRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationDegPerSecond * Time.deltaTime);
    }
}
