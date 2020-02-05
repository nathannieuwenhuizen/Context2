using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetroGen : MonoBehaviour {

    [SerializeField] float pieceDistance = 3;
    [SerializeField] GameObject[] piecePrefabs;
    public float pieceCount = 2;

    private void Start() {
        for (int i = 0; i < pieceCount; i++) {
            GameObject.Instantiate(piecePrefabs[Random.Range(0,piecePrefabs.Length)],transform.position + transform.forward * i * pieceDistance,Quaternion.identity,transform);
        }

        //place front and back
    }
}