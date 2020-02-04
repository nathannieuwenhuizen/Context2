using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Conductur : MonoBehaviour
{

    [SerializeField]
    private float walkSpeed; // 1.0 1.2

    [SerializeField]
    private float rotateSpeed;

    private Vector3 direction; // x, y, z
    private Rigidbody rb;

    [SerializeField]
    private Transform cameraTransform;

    private Vector3 mousePos;
    private Vector3 mouseDelta;

    private Traveler selectedTraveler;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        mousePos = Input.mousePosition;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CheckClickEvent();
        Move();
        Rotate();
    }
    private void CheckClickEvent()
    {
        Vector3 fwd = cameraTransform.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(cameraTransform.transform.position, fwd * 50, Color.green);
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit objectHit;
            if (Physics.Raycast(cameraTransform.transform.position, fwd, out objectHit, 50))
            {
                //do something if hit object ie
                if (objectHit.transform.parent.GetComponent<Traveler>())
                {
                    selectedTraveler = objectHit.transform.parent.GetComponent<Traveler>();
                    selectedTraveler.ShowMenu();
                }

            } else if (selectedTraveler != null)
            {
                selectedTraveler.HideMenu();
            }
        }

    }
    private Vector2 getMouseDelta()
    {
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }
    private void Move()
    {
        //walk
        direction = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        );
        transform.Translate(direction * walkSpeed);
    }

    private void Rotate()
    {
        //rotate
        transform.Rotate(new Vector3(0, getMouseDelta().x * rotateSpeed, 0));
        cameraTransform.Rotate(new Vector3(-getMouseDelta().y * rotateSpeed, 0, 0));

    }
}

 
public class CustomInputModule : StandaloneInputModule
{
    // Current cursor lock state (memory cache)
    private CursorLockMode _currentLockState = CursorLockMode.None;

    /// <summary>
    /// Process the current tick for the module.
    /// </summary>
    public override void Process()
    {
        _currentLockState = Cursor.lockState;

        Cursor.lockState = CursorLockMode.None;

        base.Process();

        Cursor.lockState = _currentLockState;
    }
}
