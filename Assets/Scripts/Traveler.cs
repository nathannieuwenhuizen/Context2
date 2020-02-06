using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Traveler : MonoBehaviour
{
    [SerializeField]
    private Appearance appearance;
    private bool ticketIsValid = true;

    private bool fined = false;
    private bool ticketChecked = false;

    [SerializeField]
    private GameObject optionMenu;

    [SerializeField]
    private Transform pivotMenu;
    [SerializeField]
    private float rotationDamping = 10;

    [Header("Appearance info")]
    [SerializeField]
    private MeshRenderer meshRenderer;
    
    [Header("UI sprites")]
    [SerializeField]
    private Sprite correct;
    [SerializeField]
    private Sprite incorrect;
    [SerializeField]
    private Image statusImage;
    [SerializeField]
    private GameObject statusObject;
    [SerializeField]
    private Text dialogueText;
    [SerializeField]
    private float dialogueDuration = 0.5f;

    private bool menuIsShown;

    private AudioSource audioS;
    void Start()
    {
        HideMenu();
        statusObject.SetActive(false);

        audioS = GetComponent<AudioSource>();

        //appearance = new Appearance();
        //appearance.Randomnize();
        //ApplyAppearance();
    }

    public void ShowMenu()
    {
        Talk(Data.GetRandomFromList(Data.greetingDialogues));
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
        switch (appearance.clothes)
        {
            case Clothes.black:
                meshRenderer.material.color = new Color(0, 0, 0);
                break;
            case Clothes.blue:
                meshRenderer.material.color = new Color(0, 0, 1);
                break;
            case Clothes.red:
                meshRenderer.material.color = new Color(1, 0, 0);
                break;
            case Clothes.green:
                Debug.Log("Green");
                meshRenderer.material.color = new Color(0, 1, 0);
                break;
            case Clothes.yellow:
                meshRenderer.material.color = new Color(0, 1, 1);
                break;
            default:
                meshRenderer.material.color = new Color(0, 0, 0);
                break;
        }
    }

    public void CheckTicket()
    {
        if (ticketChecked) { return; }

        audioS.Play();
        Talk(Data.GetRandomFromList(Data.checkedDialogue));
        ticketChecked = true;
        statusObject.SetActive(true);
        statusImage.sprite = ticketIsValid ? correct : incorrect;
    }

    public Appearance Appearance
    {
        get { return appearance; }
        set {
            appearance = value;
            ApplyAppearance();
        }
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

    public void Talk(string line)
    {
        dialogueText.text = "";
        StartCoroutine(Talking(line));
    }
    private IEnumerator Talking(string line)
    {
        float interval = dialogueDuration / line.Length;
        for (int i = 0; i < line.Length; i++)
        {
            dialogueText.text += line[i];
            yield return new WaitForSeconds(interval);
        }
    }

}

public class Appearance {
    public Clothes clothes;
    //public HairColor hairColor;
    //public HairStyle hairStyle;

    public void Randomnize()
    {
        clothes = GetRandomEnum<Clothes>();
        //hairColor = GetRandomEnum<HairColor>();
        //hairStyle = GetRandomEnum<HairStyle>();
    }
    public static T GetRandomEnum<T>()
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
    blue,
    yellow,
    green,
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