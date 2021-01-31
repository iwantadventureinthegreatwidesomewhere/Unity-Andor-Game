using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle2 : MonoBehaviour
{
    private bool isVisible = false;
    public GameObject obj;

    public void toggle()
    {
        if (isVisible)
        {
            obj.SetActive(false);
            isVisible = false;

        }
        else
        {
            obj.SetActive(true);
            isVisible = true;

        }
    }
}
