using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingColourHandler : MonoBehaviour
{
    [SerializeField]
    private Color primaryColor = Color.white;
    private ParticleSystem[] particleSystems;
    // Start is called before the first frame update
    void Start()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        applyCustomColor();
    }

    private void applyCustomColor()
    {
        for (int i = 0; i < particleSystems.Length; i++)
        {
            ParticleSystem particle = particleSystems[i];
            var colorModule = particle.colorOverLifetime;
            Gradient currentGradient = colorModule.color.gradient;
            Gradient updatedGradient = new Gradient();
            updateGradientKeys(currentGradient, updatedGradient);
            colorModule.color = updatedGradient;
        }
    }

    private void updateGradientKeys(Gradient currentGradient, Gradient updatedGradient)
    {
        GradientColorKey[] updatedColorKeys = new GradientColorKey[currentGradient.colorKeys.Length];
        for (int i = 0; i < currentGradient.colorKeys.Length; i++)
        {
            GradientColorKey key = currentGradient.colorKeys[i]; 
            Color newKeyColor = primaryColor;
            if (key.color == Color.white)
            {
                newKeyColor = Color.white;
            }
            updatedColorKeys[i] = new GradientColorKey(newKeyColor, currentGradient.colorKeys[i].time);
        }

        GradientAlphaKey[] updatedAlphaKeys = new GradientAlphaKey[currentGradient.alphaKeys.Length];
        for (int i = 0; i < currentGradient.alphaKeys.Length; i++)
        {
            updatedAlphaKeys[i] = new GradientAlphaKey(currentGradient.alphaKeys[i].alpha, currentGradient.alphaKeys[i].time);
        }
        updatedGradient.SetKeys(updatedColorKeys, updatedAlphaKeys);
    }

    public void UpdateRingColor(Color colorToSet)
    {
        primaryColor = colorToSet;
    }
}
