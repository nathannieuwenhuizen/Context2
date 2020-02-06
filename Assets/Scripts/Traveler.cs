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
    private SkinnedMeshRenderer[] meshRenderers;
    
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
    private GameObject dialogueObject;
    [SerializeField]
    private Text dialogueText;
    [SerializeField]
    private float dialogueDuration = 0.5f;
    [SerializeField]
    private Image checkingTimer;
    [SerializeField]
    private float checkingSpeed = 1f;
    [SerializeField]
    private GameObject checkButton;
    [SerializeField]
    private Text fineButton;
    [SerializeField]
    private GameObject fineMessage;

    public bool menuIsShown;

    private AudioSource audioS;
    void Start()
    {
        HideMenu();
        statusObject.SetActive(false);
        fineMessage.SetActive(false);
        dialogueObject.SetActive(false);
        checkingTimer.gameObject.SetActive(false);

        audioS = GetComponent<AudioSource>();
    }

    public void ShowMenu()
    {
        //Talk(Data.GetRandomFromList(Data.greetingDialogues));
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
                ShirtColor = new Color(0, 0, 0);
                break;
            case Clothes.blue:
                ShirtColor = new Color(0, 0, 1);
                break;
            case Clothes.red:
                ShirtColor = new Color(1, 0, 0);
                break;
            case Clothes.green:
                ShirtColor = new Color(0, 1, 0);
                break;
            case Clothes.yellow:
                ShirtColor = new Color(0, 1, 1);
                break;
            default:
                ShirtColor = new Color(0, 0, 0);
                break;
        }
    }

    public void CheckTicket()
    {
        if (ticketChecked) { return; }
        ticketChecked = true;
        
        audioS.Play();
        Talk(Data.GetRandomFromList(Data.checkedDialogue));

        checkButton.SetActive(false);
        StartCoroutine(CheckingTicket());
    }
    IEnumerator CheckingTicket()
    {
        checkingTimer.gameObject.SetActive(true);
        float value = 0;

        while (value < 0.99f)
        {
            value += Time.deltaTime * checkingSpeed;
            checkingTimer.fillAmount = value;
            yield return new WaitForFixedUpdate();
        }

        checkingTimer.gameObject.SetActive(false);
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
        fined = !fined;
        fineButton.text = fined ? "Cancel Fine" : "Give Fine";
        fineMessage.SetActive(fined);


        if (fined)
        {
            Conductur.instance.GiveFine(this);
        }
        else
        {
            Conductur.instance.RetakeFine(this);
        }
        //HideMenu();
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
    public Color ShirtColor
    {
        get
        {
            if (meshRenderers.Length > 0)
            {
                return meshRenderers[0].material.color;
            } else
            {
                return new Color();
            }
        }
        set
        {
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                meshRenderers[i].material.color = value;
            }
        }
    }

    void Update()
    {
        if (menuIsShown)
        {
            var lookPos = transform.position - Conductur.instance.transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            pivotMenu.rotation = Quaternion.Slerp(pivotMenu.rotation, rotation, Time.deltaTime * rotationDamping);
        }
    }

    public void Talk(string line)
    {
        dialogueObject.SetActive(true);
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