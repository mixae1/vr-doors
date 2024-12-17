using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AN_DoorKey : MonoBehaviour
{
    public InputActionReference interactAction;
    public AN_DoorScript doorObject;

    // NearView()
    float distance;
    float angleView;
    Vector3 direction;

    private void Start()
    {
    }

    void Update()
    {
        if ( NearView() && interactAction.action.IsPressed())
        {
            doorObject.isOpened = true;
            Destroy(gameObject);
        }
    }

    bool NearView() // it is true if you near interactive object
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        direction = transform.position - Camera.main.transform.position;
        angleView = Vector3.Angle(Camera.main.transform.forward, direction);
        if (distance < 2f) return true; // angleView < 35f && 
        else return false;
    }
}
