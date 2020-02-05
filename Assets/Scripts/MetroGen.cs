using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetroGen : MonoBehaviour {

    public static MetroGen instance;

    [Header("metro info")]
    [SerializeField] float pieceDistance = 3;
    [SerializeField] GameObject[] piecePrefabs;
    [SerializeField] GameObject gatePiecePrefab;
    [SerializeField] GameObject doorPiecePrefab;
    [SerializeField] int gateInterval = 3;
    [SerializeField] GameObject endTriggerprefab;
    public float pieceCount = 2;

    [Header("sit places info")]
    [SerializeField] GameObject gatePieceSitpointsPrefab;
    [SerializeField] GameObject pieceSitpointsPrefab;
    private List<Transform> sitPoints;

    [Header("traveler info")]
    [SerializeField] int amountOfTravelers = 10;
    [SerializeField] GameObject travelerPrefab;
    public List<Traveler> travelers;

    private void Start() {
        instance = this;

        GenerateMetro();
        GenerateTravelers();

    }

    public void GenerateMetro()
    {
        sitPoints = new List<Transform>();

        //back
        GameObject.Instantiate(doorPiecePrefab, transform.position + transform.forward * -1 * pieceDistance, Quaternion.identity, transform);

        //between parts
        for (int i = 0; i < pieceCount; i++)
        {
            if (i % 3 == 0)
            {
                GameObject.Instantiate(gatePiecePrefab, transform.position + transform.forward * i * pieceDistance, Quaternion.identity, transform);
                GameObject parentSitpoint =  GameObject.Instantiate(gatePieceSitpointsPrefab, transform.position + transform.forward * i * pieceDistance, Quaternion.identity, transform);
                foreach(Transform sitpoint in parentSitpoint.GetComponentsInChildren<Transform>())
                {
                    sitPoints.Add(sitpoint);
                }
                sitPoints.Remove(parentSitpoint.transform);
            }
            else
            {
                GameObject.Instantiate(piecePrefabs[Random.Range(0, piecePrefabs.Length)], transform.position + transform.forward * i * pieceDistance, Quaternion.identity, transform);
                GameObject parentSitpoint = GameObject.Instantiate(pieceSitpointsPrefab, transform.position + transform.forward * i * pieceDistance, Quaternion.identity, transform);
                foreach (Transform sitpoint in parentSitpoint.GetComponentsInChildren<Transform>())
                {
                    sitPoints.Add(sitpoint);
                }
                sitPoints.Remove(parentSitpoint.transform);
            }
        }

        //front
        GameObject.Instantiate(doorPiecePrefab, transform.position + transform.forward * pieceCount * pieceDistance, Quaternion.identity, transform);
        GameObject.Instantiate(endTriggerprefab, transform.position + transform.forward * pieceCount * pieceDistance, Quaternion.identity, transform);
    }

    public void GenerateTravelers()
    {
        GameObject travelerParent = new GameObject("travelers");

        travelers = new List<Traveler>();
        for (int i = 0; i < amountOfTravelers; i++)
        {

            int randomIndex = Random.Range(0, sitPoints.Count);
            Vector3 spawnPos = sitPoints[randomIndex].position;
            sitPoints.Remove(sitPoints[randomIndex]);

            Traveler traveler = GameObject.Instantiate(
                travelerPrefab, spawnPos, Quaternion.identity, travelerParent.transform)
                .GetComponent<Traveler>();
            travelers.Add(traveler);

            if (Random.Range(0, 100) > 50)
            {
                traveler.TicketIsValid = false;
            }
        }
    }
}