using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AN_DoorScript : MonoBehaviour
{
    [Tooltip("If it is false door can't be used")]
    public bool Locked = false;
    [Tooltip("It is true for remote control only")]
    public bool Remote = false;
    [Space]
    [Tooltip("Door can be opened")]
    public bool CanOpen = true;
    [Tooltip("Door can be closed")]
    public bool CanClose = true;
    [Space]
    [Tooltip("Door locked by red key (use key script to declarate any object as key)")]
    public bool RedLocked = false;
    public bool BlueLocked = false;
    [Tooltip("It is used for key script working")]
    AN_HeroInteractive HeroInteractive;
    [Space]
    public bool isOpened = false;
    [Range(0f, 4f)]
    [Tooltip("Speed for door opening, degrees per sec")]
    public float OpenSpeed = 3f;

    public InputActionReference interactAction;

    // NearView()
    float distance;
    float angleView;
    Vector3 direction;

    // Hinge
    [HideInInspector]
    public Rigidbody rbDoor;
    HingeJoint hinge;
    JointLimits hingeLim;
    float currentLim;

    void Start()
    {
        rbDoor = GetComponent<Rigidbody>();
        hinge = GetComponent<HingeJoint>();
        HeroInteractive = FindObjectOfType<AN_HeroInteractive>();
    }

    void Update()
    {
        if ( !Remote && interactAction.action.IsPressed() && NearView() )
            Action();
        
    }

    public void Action() // void to open/close door
    {
        Debug.Log("Door Action!");
        if (!Locked)
        {
            Debug.Log("!Locked!");
            // key lock checking
            if (HeroInteractive != null && RedLocked && HeroInteractive.RedKey)
            {
                Debug.Log("IF 1");
                RedLocked = false;
                HeroInteractive.RedKey = false;
            }
            else if (HeroInteractive != null && BlueLocked && HeroInteractive.BlueKey)
            {
                Debug.Log("IF 2");
                BlueLocked = false;
                HeroInteractive.BlueKey = false;
            }
            
            // opening/closing
            if (isOpened && CanClose && !RedLocked && !BlueLocked)
            {
                Debug.Log("IF 3");
                isOpened = false;
            }
            else if (!isOpened && CanOpen && !RedLocked && !BlueLocked)
            {
                Debug.Log("IF 4");
                isOpened = true;
                rbDoor.AddRelativeTorque(new Vector3(0, 0, 20f)); 
            }
        
        }
    }

    bool NearView() // it is true if you near interactive object
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        direction = transform.position - Camera.main.transform.position;
        angleView = Vector3.Angle(Camera.main.transform.forward, direction);
        if (distance < 3f) return true; // angleView < 35f && 
        else return false;
    }

    private void FixedUpdate() // door is physical object
    {
        if (isOpened)
        {
            currentLim = 85f;
        }
        else
        {
            // currentLim = hinge.angle; // door will closed from current opened angle
            if (currentLim > 1f)
                currentLim -= .5f * OpenSpeed;
        }

        // using values to door object
        hingeLim.max = currentLim;
        hingeLim.min = -currentLim;
        hinge.limits = hingeLim;
    }
}
