using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public bool moving;
    public float speed = 1;
    public float maxDist = 200;

    Vector3 startpos;

    private void Start() {
        startpos = transform.position;
    }

    void Update() {
        if (moving) transform.position += transform.forward * speed * Time.deltaTime;
        if (Vector3.Distance(startpos,transform.position) > maxDist) transform.position = startpos;
    }
}
