using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class RuneStoneButton : MonoBehaviour
    {
        public bool Pressed { get; set; }

        public void OnButtonPressed()
        {
            Pressed = true;
            GameObject.Find("RunestoneUI").transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
