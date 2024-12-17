using UnityEngine;
using UnityEngine.InputSystem;

public class AN_Button : MonoBehaviour
{
    [Tooltip("If it isn't valve, it can be lever or button (animated)")]
    public bool isLever = true;
    [Tooltip("The door for remote control")]
    public AN_DoorScript DoorObject;
    [Tooltip("Current status of the door")]
    public bool isOpened = false;
    [Space]
    [Tooltip("True for rotation by X local rotation by valve")]
    public bool xRotation = true;
    [Tooltip("True for vertical movenment by valve (if xRotation is false)")]
    public bool yPosition = false;
    public float max = 90f, min = 0f, speed = 5f;

    public InputActionReference interactAction;

    Animator anim;

    // NearView()
    float distance;
    float angleView;
    Vector3 direction;

    float ltt;

    void Start()
    {
        anim = GetComponent<Animator>();
        ltt = 0.0f;
        DoorObject.isOpened = isOpened;
    }

    void Update()
    {
        if (interactAction.action.IsPressed() && Time.time - ltt > 1f && DoorObject != null && NearView()) {
            ltt = Time.time;
            isOpened = !isOpened;
            DoorObject.isOpened = isOpened;
            if (isLever) // animations
            {
                if (DoorObject.isOpened) anim.SetBool("LeverUp", true);
                else anim.SetBool("LeverUp", false);
            }
            else anim.SetTrigger("ButtonPress");
        }
    }

    bool NearView() // it is true if you near interactive object
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        direction = transform.position - Camera.main.transform.position;
        angleView = Vector3.Angle(Camera.main.transform.forward, direction);
        if (angleView < 45f && distance < 2f) return true;
        else return false;
    }
}
