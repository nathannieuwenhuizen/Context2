using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{

    [Header("UI data text")]
    [SerializeField] private Text dataText;
    [SerializeField] private Text corrFinedText;
    [SerializeField] private Text corrFinedPointsText;
    [SerializeField] private Text wronSkippedText;
    [SerializeField] private Text wronSkippedPointsText;
    [SerializeField] private Text wronFinedText;
    [SerializeField] private Text wronFinedPointsText;
    [SerializeField] private Text timeText;
    [SerializeField] private Text timePointsText;
    [SerializeField] private Text timeFormattedText;
    [SerializeField] private Text playerScoreText;

    [Header("Score tweak")]
    [SerializeField] float verkeerdGeskiptGewicht = -1;
    [SerializeField] float correctBeboetGewicht = 1;
    [SerializeField] float verkeerBeboetGewicht = -3;
    [SerializeField] float tijdGewicht = 100;
    [SerializeField] int scoreMultiplier = 1000;

    [Header("Fake Highscores")]
    [SerializeField] float highScoreFirst = 17104;

    [Header("Final level")]
    [SerializeField] private GameObject quitButton;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private Text endNote;
    [SerializeField] private Image endNoteBackground;

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
            quitButton.SetActive(true);
            continueButton.SetActive(false);
            endNote.gameObject.SetActive(true);
            endNoteBackground.gameObject.SetActive(true);
            if (Data.amountIlligalMissed > 0)
            {
                endNote.text = "IMPORTANT! \n" +
                    "Your performance has been measured and you have incorrectly fined passengers with valid tickets based on prejudice! You have been FIRED because of discrimination!";
            } else
            {
                endNote.text = "IMPORTANT! \n" +
                    "The inspector has measured your performance and you are found popular and exact! You're PROMOTED!";

            }
        } else
        {
            quitButton.SetActive(false);
            endNote.gameObject.SetActive(false);
            endNoteBackground.gameObject.SetActive(false);
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
            };

        corrFinedText.text = corrFined.ToString();
        corrFinedPointsText.text = corrFinedPoints.ToString();
        wronSkippedText.text = wronSkipped.ToString();
        wronSkippedPointsText.text = wronSkippedPoints.ToString();
        wronFinedText.text = wronFined.ToString();
        wronFinedPointsText.text = wronFinedPoints.ToString();
        timeText.text = Mathf.Ceil(Data.timePassed).ToString();
        timePointsText.text = timePoints.ToString();
        timeFormattedText.text = timeTaken;
        playerScoreText.text = totalScore.ToString();

    }

    void Update()
    {
        
    }
}
