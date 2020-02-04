using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traveler : MonoBehaviour
{
    Appearance appearance;
    public bool ticketIsValid = true;

    [SerializeField]
    private GameObject optionMenu;
    void Start()
    {
        HideMenu();
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