using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateClicker : MonoBehaviour
{

    public TMP_Text SusageClickText;
    private int SusageClickCounter;


    public void AddClicks()
    {
        SusageClickCounter++;
        SusageClickText.text = SusageClickCounter.ToString();
    }
}
