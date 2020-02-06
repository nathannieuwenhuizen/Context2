using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Conductur : MonoBehaviour
{
    //fine data
    public List<Traveler> finedTravelers;

    public static Conductur instance;

    private Rigidbody rb;

    [SerializeField]
    private Transform cameraTransform;

    private Traveler selectedTraveler;
    private AudioSource audioS;

    void Start()
    {
        finedTravelers = new List<Traveler>();
        rb = GetComponent<Rigidbody>();
        audioS = GetComponent<AudioSource>();
        
        instance = this;
    }

    void Update()
    {
        CheckClickEvent();
    }
    private void CheckClickEvent()
    {
        Vector3 fwd = cameraTransform.transform.TransformDirection(Vector3.forward);
        if (Input.GetButtonDown("Fire1"))
        {
            //check object hit
            RaycastHit objectHit;
            if (Physics.Raycast(cameraTransform.transform.position, fwd, out objectHit, 50))
            {
                //check parent of box coll
                if (objectHit.transform.parent != null)
                {
                    //check traveler
                    if (objectHit.transform.parent.GetComponent<Traveler>())
                    {
                        audioS.Play();

                        SelectedTraveler = objectHit.transform.parent.GetComponent<Traveler>();
                        SelectedTraveler.ShowMenu();

                    }
                    //check canvas coll
                    else if (objectHit.transform.parent.parent != null)
                    {
                        if (selectedTraveler != objectHit.transform.parent.parent.GetComponent<Traveler>())
                        {
                            //NoTravelerSelected();
                        }
                    } else
                    {
                        //NoTravelerSelected();
                    }
                } else
                {
                    //NoTravelerSelected();
                }
            } else
            {
                //NoTravelerSelected();
            }
        }

    }
    private void NoTravelerSelected()
    {
        if (selectedTraveler != null)
        {
            selectedTraveler.HideMenu();
        }
    }

    public Traveler SelectedTraveler
    {
        get { return selectedTraveler; }
        set
        {
            if (selectedTraveler != null)
            {
                selectedTraveler.HideMenu();
            }
            selectedTraveler = value;
        }
    }
    public void GiveFine(Traveler appearance)
    {
        finedTravelers.Add(appearance);
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


    public void CalculateResult()
    {
        //MetroGen.instance.travelers.Count
    }
}
