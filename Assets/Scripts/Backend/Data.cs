using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    public static List<Traveler> finedAppearances = new List<Traveler>();
    public static float timePassed = 0;
    public static float precentageChecked;
    public static int amountFined;
    public static int amountWronglyFined;
    public static int amountIlligalMissed;

    public static int day = 1;
    public static bool finalLevel = false;

    public static Clothes patternIndex = Appearance.GetRandomEnum<Clothes>();
    //dialogues
    public static List<string> greetingDialogues = new List<string>
    {
        "Hello",
        "Good day sir",
        "Hey conductur",
        "Good afternoon",
        "Sup",
        "Do you want my ticket?",
        "Hey, how are you?"
    };
    public static List<string> checkedDialogue = new List<string>
    {
        "Let me check where I have my ticket… ",
        "I want to be home soon to be there when my food box arrives! The delivery railway system is so perfect, right? ",
        "I’m on my way to visit my family across the country, they have these new cardboard houses! ",
        "Hey, good luck checking the tickets of all those passengers! Pretty busy train! Let me find mine.",
        "You like my new shirt? ",
        "I’m having a bad day, hmpf, where is my ticket… ",
        "Let me tell you about my neighbour, she was feeling so ill. She took one of those health pills and everything is good! It is amazing! ",
        "Good afternoon, don’t you just love life?",
        "My cat died yesterday… Luckily I can ask Generate Your Beast to generate exactly the same cat as I had! What a time to be alive eh. ",
        "We live in a society"
    };

    public static string GetRandomFromList(List<string> list)
    {
        int index = Random.Range(0, list.Count);
        return list[index];
    }
}
