using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{

    [Header("UI data text")]
    [SerializeField] private Text dataText;
    [SerializeField] private Text scoresText;

    [Header("Score tweak")]
    [SerializeField] float verkeerdGeskiptGewicht = -1;
    [SerializeField] float correctBeboetGewicht = 1;
    [SerializeField] float verkeerBeboetGewicht = -3;
    [SerializeField] float tijdGewicht = 100;
    [SerializeField] int scoreMultiplier = 1000;

    [Header("Fake Highscores")]
    [SerializeField] float highScoreFirst = 17104;

    [Header("Final level")]
    [SerializeField]
    private GameObject quitButton;
    [SerializeField] 
    private GameObject continueButton;
    [SerializeField]
    private Text endNote;

    // amount fined = iedereen die je hebt beboet
    // percentagechecked = percentage die je hebt hebt gecontroleerd
    // wrongly fined = mensen die je verkeerd hebt beboet
    // illigal missed = mensen die je verkeer hebt geskipt
    // timePassed = stopwatch time

    void Start()
    {
        Data.day++;

        if (Data.finalLevel)
        {

        }

        var minutes = Data.timePassed / 60;
        var seconds = Data.timePassed % 60;

        string timeTaken = string.Format("{0:00}:{1:00}", Mathf.Floor(minutes), seconds);

        var corrFined = (Data.amountFined - Data.amountWronglyFined);
        var wronFined = Data.amountWronglyFined;
        var wronSkipped = Data.amountIlligalMissed;

        var corrFinedPoints = Mathf.Floor(corrFined * correctBeboetGewicht * scoreMultiplier);
        var wronFinedPoints = Mathf.Floor(wronFined * verkeerBeboetGewicht * scoreMultiplier);
        var wronSkippedPoints = Mathf.Floor(wronSkipped * verkeerdGeskiptGewicht * scoreMultiplier);
        var timePoints = Mathf.Floor(tijdGewicht / Data.timePassed * scoreMultiplier);
        var totalScore = corrFinedPoints + wronFinedPoints + wronSkippedPoints + timePoints;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        dataText.text =
            timeTaken + "\n\n" +

            corrFined + " = " + corrFinedPoints + "\n" +
            wronFined + " = " + wronFinedPoints + "\n" +
            wronSkipped + " = " + wronSkippedPoints + "\n" +
            timePoints + "\n\n" +

            totalScore + "\n\n" +

            
            (totalScore > highScoreFirst ? totalScore : highScoreFirst);
            if (totalScore > highScoreFirst)
            {
                Data.finalLevel = true;
            }
    }

    void Update()
    {
        
    }
}
