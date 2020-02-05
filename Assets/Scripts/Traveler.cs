using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Traveler : MonoBehaviour
{
    Appearance appearance;
    public bool ticketIsValid = true;

    private bool recievedFine = false;
    private bool ticketChecked = false;

    [SerializeField]
    private GameObject optionMenu;

    [Header("UI sprites")]
    [SerializeField]
    private Sprite correct;
    [SerializeField]
    private Sprite incorrect;
    [SerializeField]
    private Image statusImage;
    [SerializeField]
    private GameObject statusObject;

    void Start()
    {
        HideMenu();
        statusObject.SetActive(false);

        appearance = new Appearance();
        appearance.Randomnize();
        ApplyAppearance();
    }

    public void ShowMenu()
    {
        optionMenu.SetActive(true);
    }
    public void HideMenu()
    {
        optionMenu.SetActive(false);
    }
    public void ApplyAppearance()
    {

    }

    public void CheckTicket()
    {
        if (ticketChecked) { return; }

        Debug.Log("ticket checked");

        ticketChecked = true;
        statusObject.SetActive(true);
        statusImage.sprite = ticketIsValid ? correct : incorrect;
    }

    public void RecieveFine()
    {
        if (recievedFine) { return; }

        Debug.Log("recieved Fine");

        recievedFine = true;
        Conductur.instance.GiveFine(appearance);
        HideMenu();
    }

    void Update()
    {
        
    }
}

public class Appearance {
    public Clothes clothes;
    public HairColor hairColor;
    public HairStyle hairStyle;

    public void Randomnize()
    {
        clothes = GetRandomEnum<Clothes>();
        hairColor = GetRandomEnum<HairColor>();
        hairStyle = GetRandomEnum<HairStyle>();
    }
    static T GetRandomEnum<T>()
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
        return V;
    }
}


public enum Clothes
{
    red,
    black,
    blue
}
public enum HairColor
{
    black,
    brown,
    blond
} 
public enum HairStyle
{
    bald,
    shortHair,
    longHair
}