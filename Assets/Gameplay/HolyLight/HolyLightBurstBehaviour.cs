using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyLightBurstBehaviour : MonoBehaviour
{
    [SerializeField]
    private Animator lightAnimator;
    private float burstAnimationLength = 0.4f;
    private string triggerName = "Emit";
    private bool isEmitting = false;
    public void EmitLight()
    {
        if (!isEmitting)
        {
            isEmitting = true;
            StartCoroutine(emitLightRoutine());
        }
    }

    private IEnumerator emitLightRoutine()
    {
        lightAnimator.SetTrigger(triggerName);
        yield return new WaitForSeconds(burstAnimationLength);
        isEmitting = false;
    }
}
