using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VRTemplate;
using System;

public class AN_DoorScript : MonoBehaviour
{
    public bool isOpened = true;

    XRKnob knob;

    void Start()
    {
        knob = GetComponent<XRKnob>();
    }

    private void FixedUpdate()
    {
        if(knob.enabled != isOpened) {
            if(!isOpened) knob.value = 0.5f;
            knob.enabled = isOpened;
        }
    }
}
