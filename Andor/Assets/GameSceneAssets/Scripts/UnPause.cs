using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnPause : MonoBehaviour
{
    public GameObject pausebtn;
    public GameObject pauseMenu;

    public void unpause()
    {

        pausebtn.SetActive(true);
        pauseMenu.SetActive(false);


    }
}
