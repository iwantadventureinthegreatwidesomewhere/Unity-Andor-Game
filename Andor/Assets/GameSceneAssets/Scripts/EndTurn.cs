using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurn : MonoBehaviour
{
    public GameObject moveBtn, fightBtn;
    public Text turnteller;

    public void endTurn()
    {
        moveBtn.SetActive(false);
        fightBtn.SetActive(false);
        turnteller.text = "Next Player";
    }

}
