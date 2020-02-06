using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{

    [Header("UI data text")]
    [SerializeField]
    private Text dataText;
    void Start()
    {

        var minutes = Data.timePassed / 60;
        var seconds = Data.timePassed % 60;

        string timeLeft = string.Format("{0:00}:{1:00}", Mathf.Floor(minutes), seconds);

        Cursor.lockState = CursorLockMode.None;
        dataText.text =
            timeLeft + " \n" +
            Data.amountFined + "\n" +
            Data.precentageChecked + "% \n" +
            Data.amountWronglyFined + "\n" +
            Data.amountIlligalMissed + "\n";
          
    }

    void Update()
    {
        
    }
}
