using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMovement : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private float rotateSpeed;

    [SerializeField]
    private Transform cameraTransform;
    private Vector3 direction;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private Vector2 getMouseDelta()
    {
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }
    private void Rotate()
    {
        transform.Rotate(new Vector3(0, getMouseDelta().x * rotateSpeed * Time.deltaTime, 0));
        cameraTransform.Rotate(new Vector3(-getMouseDelta().y * rotateSpeed * Time.deltaTime, 0, 0));

    }

    private void Move()
    {
        direction = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        );
        transform.Translate(direction * walkSpeed);
    }

    private void Update()
    {
        
    }

    void FixedUpdate()
    {
        Rotate();
        Move();
    }
}
