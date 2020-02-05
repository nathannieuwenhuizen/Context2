using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Conductur : MonoBehaviour
{
    //fine data
    public List<Appearance> ticketAppearances;

    public static Conductur instance;

    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private float rotateSpeed;

    private Vector3 direction;
    private Rigidbody rb;

    [SerializeField]
    private Transform cameraTransform;

    private Vector3 mousePos;
    private Vector3 mouseDelta;

    private Traveler selectedTraveler;

    void Start()
    {
        ticketAppearances = new List<Appearance>();
        rb = GetComponent<Rigidbody>();

        instance = this;

        mousePos = Input.mousePosition;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
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

    public void GiveFine(Appearance appearance)
    {
        ticketAppearances.Add(appearance);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<End>())
        {
            other.gameObject.GetComponent<End>().ShowExitButton();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<End>())
        {
            other.gameObject.GetComponent<End>().HideExitButton();
        }
    }

    private void Rotate()
    {
        //rotate
        transform.Rotate(new Vector3(0, getMouseDelta().x * rotateSpeed, 0));
        cameraTransform.Rotate(new Vector3(-getMouseDelta().y * rotateSpeed, 0, 0));

    }
}
