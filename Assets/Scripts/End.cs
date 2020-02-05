using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class End : MonoBehaviour
{

    [SerializeField]
    private GameObject exitButton;

    public void Start()
    {
        HideExitButton();
        exitButton.GetComponent<Button>().onClick.AddListener(GameManager.instance.End);
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
