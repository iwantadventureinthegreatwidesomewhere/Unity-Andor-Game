using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle1 : MonoBehaviour
{

    private bool isVisible = true;
    public GameObject obj;

    public void toggle() 
    {
        if (isVisible)
        {
            isVisible = false;
            obj.SetActive(false);
            
        } else
        {
            isVisible = true;
            obj.SetActive(true);
           
        }
    }
    
}
