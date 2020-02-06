using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public Stopwatch stopwatch;

    [SerializeField]
    private GameObject dayText;

    [SerializeField]
    private GameObject baseLevel;
    [SerializeField]
    private GameObject finalLevel;
    private void Awake()
    {
        instance = this;
        baseLevel.SetActive(!Data.finalLevel);
        finalLevel.SetActive(Data.finalLevel);

    }

    public void Start()
    {
        dayText.GetComponent<Text>().text = "Day " + Data.day;
        Destroy(dayText, 2f);
    }

    public void End()
    {
        Data.amountFined = Conductur.instance.finedTravelers.Count;
        Data.timePassed = stopwatch.TimePassed;
        Data.amountWronglyFined = GetAmountWronglyFined();
        Data.amountIlligalMissed = GetAmountIllegalsMissed();
        Data.finedAppearances = Conductur.instance.finedTravelers;
        Data.precentageChecked = GetPrecentageChecked();

        GetComponent<SceneLoader>().LoadSceneByBuildIndex(2);
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
