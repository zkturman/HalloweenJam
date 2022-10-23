using System.Collections;
using UnityEngine;

public class SkullOverworldBehaviour : MonoBehaviour
{
    [SerializeField]
    private Rigidbody skullRigidBody;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private AudioSource collectSoundPlayer;
    [SerializeField]
    private AudioClip collectSound;
    private float clipLengthInSeconds = 2f;

    // Start is called before the first frame update
    void Start()
    {
        skullRigidBody.angularVelocity = Vector3.up * rotationSpeed;
    }

    public void PickupSkull()
    {
        StartCoroutine(destorySkull());
    }

    private IEnumerator destorySkull()
    {
        collectSoundPlayer.clip = collectSound;
        collectSoundPlayer.Play();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        yield return new WaitForSeconds(clipLengthInSeconds);
        Destroy(this.gameObject);
    }
}
