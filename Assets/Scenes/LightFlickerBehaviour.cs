using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickerBehaviour : MonoBehaviour
{
    [SerializeField]
    private Light spotlight;
    [SerializeField]
    private Light pointLight;
    private float baseSpotlightIntensity;
    private float basePointLightIntensity;
    [SerializeField]
    [Range(0, 10)]
    private int flickerRate;
    private int maxFlickerRate = 10;
    private bool lightFlickering = false;

    // Start is called before the first frame update
    void Start()
    {
        baseSpotlightIntensity = spotlight.intensity;
        basePointLightIntensity = pointLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (!lightFlickering)
        {
            lightFlickering = true;
            StartCoroutine(flicker());
        }
    }

    private IEnumerator flicker()
    {
        float routineTime = 0f;
        while(routineTime < 1f)
        {
            float timeStep = 0.1f;
            routineTime += timeStep;
            int diceRoll = Random.Range(0, maxFlickerRate);
            if (diceRoll < flickerRate)
            {
                if (Mathf.Approximately(spotlight.intensity, baseSpotlightIntensity))
                {
                    spotlight.intensity = 0f;
                    pointLight.intensity = 0f;
                }
                else
                {
                    spotlight.intensity = baseSpotlightIntensity;
                    pointLight.intensity = basePointLightIntensity;
                }
            }
            yield return new WaitForSeconds(timeStep);

        }
        lightFlickering = false;
    }
}
