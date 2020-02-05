using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [Header("General info")]
    [SerializeField]
    private float durationInSec = 60;
    [SerializeField]
    private UnityEvent OnTimerUp;

    private Slider slider;

    private float timePassed = 0;
    private bool isRunning = true;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = durationInSec;
    }

    void Update()
    {
        if (!isRunning) { return; }
        timePassed += Time.deltaTime;

        slider.value = timePassed;

        if (timePassed > durationInSec)
        {
            OnTimerUp.Invoke();
            StartCoroutine(TimesUp());
            isRunning = false;
        }
    }

    public float timeLeft
    {
        get { return (durationInSec - timePassed); }
    }
    IEnumerator TimesUp()
    {
        yield return new WaitForSeconds(2f);
        GameManager.instance.End();
    }
}
