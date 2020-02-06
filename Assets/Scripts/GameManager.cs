using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public Stopwatch stopwatch;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
    }

    public void End()
    {
        Data.amountFined = Conductur.instance.finedTravelers.Count;
        Data.timePassed = stopwatch.TimePassed;
        Data.amountWronglyFined = GetAmountWronglyFined();
        Data.amountIlligalMissed = GetAmountIllegalsMissed();
        Data.finedAppearances = Conductur.instance.finedTravelers;
        Data.precentageChecked = GetPrecentageChecked();

        GetComponent<SceneLoader>().LoadSceneByBuildIndex(1);
    }

    public int GetAmountWronglyFined()
    {
        int result = 0;
        foreach (Traveler traveler in Conductur.instance.finedTravelers)
        {
            if (traveler.TicketIsValid)
            {
                result++;
            }
        }
        return result;
    }

    public int GetAmountIllegalsMissed()
    {
        int result = 0;
        foreach (Traveler traveler in MetroGen.instance.travelers)
        {
            if (!traveler.TicketIsValid && !traveler.RecievedFine)
            {
                result++;
            }
        }
        return result;
    }

    public float GetPrecentageChecked()
    {
        float amountChecked = 0f;
        foreach (Traveler traveler in MetroGen.instance.travelers)
        {
            Debug.Log("Ticket checked:" + traveler.TicketChecked);
            if (traveler.TicketChecked)
            {
                amountChecked++;
            }
        }
        return (amountChecked / MetroGen.instance.travelers.Count) * 100f;
    }
}
