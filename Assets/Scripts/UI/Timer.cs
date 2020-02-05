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

        //Debug.Log("Time passed: " + timePassed);

        slider.value = timePassed;

        if (timePassed > durationInSec)
        {
            Debug.Log("Time has passed!");
            OnTimerUp.Invoke();
            StartCoroutine(TimesUp());
            isRunning = false;
        }
    }
    IEnumerator TimesUp()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<SceneLoader>().LoadSceneByBuildIndex(1);
    }
}
