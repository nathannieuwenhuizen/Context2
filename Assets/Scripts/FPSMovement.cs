﻿using System.Collections;
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

    private Rigidbody rb;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
    }
    private Vector2 getMouseDelta()
    {
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }
    private void Rotate()
    {
        transform.Rotate(new Vector3(0, getMouseDelta().x * rotateSpeed, 0));
        cameraTransform.Rotate(new Vector3(-getMouseDelta().y * rotateSpeed, 0, 0));

    }

    private void Move()
    {
        direction = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        );
        //direction = (transform.right * direction.x + transform.forward * direction.y).normalized;
        //direction.y = 0;
        //rb.MovePosition(transform.position + direction * walkSpeed);
        transform.Translate(direction * walkSpeed);
    }

    private void Update()
    {
        Rotate();
    }

    void FixedUpdate()
    {
        Move();
    }
}
