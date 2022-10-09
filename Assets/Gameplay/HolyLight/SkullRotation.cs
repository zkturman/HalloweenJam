using UnityEngine;

public class SkullRotation : MonoBehaviour
{
    [SerializeField]
    private Rigidbody skullRigidBody;
    [SerializeField]
    private float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        skullRigidBody.angularVelocity = Vector3.up * rotationSpeed;
    }
}
