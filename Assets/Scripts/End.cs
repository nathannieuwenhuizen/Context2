using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{

    [SerializeField]
    private GameObject exitButton;

    public void Start()
    {
        HideExitButton();
    }
    public void ShowExitButton()
    {
        exitButton.SetActive(true);
    }

    public void HideExitButton()
    {
        exitButton.SetActive(false);
    }

}
