using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public bool moving;
    public float speed = 1;

    void Update() {
        if (moving) {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }
}
