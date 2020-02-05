using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetroGen : MonoBehaviour {

    [SerializeField] float pieceDistance = 3;
    [SerializeField] GameObject[] piecePrefabs;
    [SerializeField] GameObject gatePiecePrefab;
    [SerializeField] GameObject doorPiecePrefab;
    [SerializeField] int gateInterval = 3;
    public float pieceCount = 2;

    private void Start() {
        GameObject.Instantiate(doorPiecePrefab, transform.position + transform.forward * -1 * pieceDistance, Quaternion.identity, transform);
        GameObject.Instantiate(doorPiecePrefab, transform.position + transform.forward * pieceCount * pieceDistance, Quaternion.identity, transform);

        for (int i = 0; i < pieceCount; i++) {
            if (i % 3 == 0) GameObject.Instantiate(gatePiecePrefab, transform.position + transform.forward * i * pieceDistance, Quaternion.identity, transform);
            else GameObject.Instantiate(piecePrefabs[Random.Range(0,piecePrefabs.Length)],transform.position + transform.forward * i * pieceDistance,Quaternion.identity,transform);
        }

        //place front and back
    }
}