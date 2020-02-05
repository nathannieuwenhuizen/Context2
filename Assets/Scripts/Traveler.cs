using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Traveler : MonoBehaviour
{
    Appearance appearance;
    private bool ticketIsValid = true;

    private bool fined = false;
    private bool ticketChecked = false;

    [SerializeField]
    private GameObject optionMenu;

    [SerializeField]
    private Transform pivotMenu;
    [SerializeField]
    private float rotationDamping = 10;

    [Header("UI sprites")]
    [SerializeField]
    private Sprite correct;
    [SerializeField]
    private Sprite incorrect;
    [SerializeField]
    private Image statusImage;
    [SerializeField]
    private GameObject statusObject;

    private bool menuIsShown;
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
        menuIsShown = true;
    }
    public void HideMenu()
    {
        optionMenu.SetActive(false);
        menuIsShown = false;
    }
    public void ApplyAppearance()
    {

    }

    public void CheckTicket()
    {
        if (ticketChecked) { return; }


        ticketChecked = true;
        statusObject.SetActive(true);
        statusImage.sprite = ticketIsValid ? correct : incorrect;
    }

    public void RecieveFine()
    {
        if (fined) { return; }


        fined = true;
        Conductur.instance.GiveFine(this);
        HideMenu();
    }
    public bool TicketIsValid
    {
        get { return ticketIsValid; }
        set { ticketIsValid = value; }
    }
    public bool RecievedFine
    {
        get { return fined; }
        set { fined = value; }
    }

    public bool TicketChecked
    {
        get { return ticketChecked; }
        set { ticketChecked = value; }
    }

    void Update()
    {
        if (menuIsShown)
        {
            var lookPos = transform.position - Conductur.instance.transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
        }
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