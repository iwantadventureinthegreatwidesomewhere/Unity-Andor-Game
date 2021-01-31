using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePause : MonoBehaviour
{
    public GameObject pausebtn;
    public GameObject pauseMenu;

    public void pause()
    {

        pausebtn.SetActive(false);   
        pauseMenu.SetActive(true);

     
    }
}
