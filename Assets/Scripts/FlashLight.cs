using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public Light flashLight;
    protected bool flashLightEnabled = false;
    public float intensity = 2f;
    public KeyCode keyCodeFlashLight = KeyCode.E;

    public float Pin;
    protected float _pin;
    protected bool hasPin;
    void Start()
    {
        _pin = Pin;
        hasPin = _pin > 0;
        flashLightEnabled = false;
        flashLight.intensity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        TurnControllerFlashLight();
        CheckPin();
    }

    private void TurnControllerFlashLight()
    {
        if (!hasPin)
        {
            flashLightEnabled = false;
            flashLight.intensity = 0;
            return;
        }
        if (flashLight == null)
            return;
        if (Input.GetKeyDown(keyCodeFlashLight))
        {
            if(!flashLightEnabled)
            {
                flashLightEnabled = true;
                flashLight.intensity = intensity;
            }
            else
            {
                flashLightEnabled = false;
                flashLight.intensity = 0;
            }
        }
    }

    private void CheckPin()
    {
        if (!flashLightEnabled) return;
        _pin -= Time.deltaTime/3;
        if(_pin > 0)
        {
            hasPin = true;
        }
        else
        {
            hasPin = false;
        }
    }
}
