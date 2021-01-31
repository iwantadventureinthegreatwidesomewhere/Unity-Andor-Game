using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class setReady : MonoBehaviour
{
    public TMP_Text textbox;

    public void playerIsReady(){

        textbox.text = "Ready!";
    }
}
