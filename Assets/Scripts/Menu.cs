using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Data.finalLevel = false;
        Data.patternIndex = Appearance.GetRandomEnum<Clothes>();
        Data.day = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
