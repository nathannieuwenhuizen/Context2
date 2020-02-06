using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Stopwatch : MonoBehaviour
{
    private Slider slider;

    private float timePassed = 0;
    private bool isRunning = true;


    private Text text;
    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        if (!isRunning) { return; }
        timePassed += Time.deltaTime;

        var minutes = timePassed / 60;
        var seconds = timePassed % 60;

        text.text = string.Format("{0:00}:{1:00}", Mathf.Floor(minutes), seconds);
    }

    public float TimePassed
    {
        get { return timePassed; }
    }
    public bool IsRunning
    {
        get { return isRunning; }
        set { isRunning = value; }
    }
    IEnumerator TimesUp()
    {
        yield return new WaitForSeconds(2f);
        GameManager.instance.End();
    }
}
