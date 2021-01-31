using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterManager : MonoBehaviour
{
    public GameObject CounterPanel;

    public void OpenPanel() {
        if (CounterPanel != null) {
            CounterPanel.SetActive(true);
        }
    }
}
