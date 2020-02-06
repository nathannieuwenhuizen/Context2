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
    [SerializeField]
    private bool randomPattern = false;
    [SerializeField]
    [Range(0, 100)]
    private float precentageCorrect = 50;
    [SerializeField]
    private int amountPatternTravelers = 5;

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
        //if (Data.patternIndex)
        //Data.patternIndex = Appearance.GetRandomEnum<Clothes>();

        GameObject travelerParent = new GameObject("travelers");

        travelers = new List<Traveler>();

        int currentAmountPatterns = 0;
        for (int i = 0; i < amountOfTravelers; i++)
        {
            if (sitPoints.Count < 2) { return; }
            
            //position
            int randomIndex = Random.Range(0, sitPoints.Count - 1);
            Vector3 spawnPos = sitPoints[randomIndex].position;
            sitPoints.Remove(sitPoints[randomIndex]);

            //instantiation
            Traveler traveler = GameObject.Instantiate(
                travelerPrefab, spawnPos, sitPoints[randomIndex].rotation, travelerParent.transform)
                .GetComponent<Traveler>();
            travelers.Add(traveler);

            //TODO: hard coded! checks on x pos
            if (traveler.transform.position.x < 0)
            {
                traveler.transform.Rotate(new Vector3(0, 180, 0));
            }

            if (randomPattern)
            {
                traveler.Appearance = new Appearance();
                traveler.Appearance.Randomnize();
                traveler.ApplyAppearance();
                if (Random.Range(0, 100) > precentageCorrect)
                {
                    traveler.TicketIsValid = false;
                }
            } else
            {
                currentAmountPatterns++;
                if (currentAmountPatterns <= amountPatternTravelers)
                {
                    traveler.Appearance = new Appearance();
                    Debug.Log("Green from gen");
                    traveler.Appearance.clothes = Data.patternIndex;
                    traveler.ApplyAppearance();

                    traveler.TicketIsValid = false;
                    traveler.gameObject.name = traveler.Appearance.clothes + "False!";
                } else
                {
                    traveler.Appearance = new Appearance();
                    traveler.Appearance.Randomnize();
                    traveler.ApplyAppearance();

                    //if there are too many patterncolor travelers
                    if (traveler.Appearance.clothes == Data.patternIndex)
                    {
                        traveler.gameObject.name = traveler.Appearance.clothes + " Should not be false!";

                        //get every other color exept the pattern color!
                        Debug.Log("no more green!");
                        int enumLength = System.Enum.GetValues(typeof(Clothes)).Length;
                        traveler.Appearance.clothes = traveler.Appearance.clothes + Random.Range(1, enumLength - 1) % enumLength;
                        traveler.ApplyAppearance();
                    } else
                    {
                        traveler.gameObject.name = traveler.Appearance.clothes + "";
                    }
                }
            }
        }
    }
}