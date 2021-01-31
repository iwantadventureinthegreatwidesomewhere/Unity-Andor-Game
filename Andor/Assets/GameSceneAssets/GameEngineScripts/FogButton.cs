using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class FogButton : MonoBehaviour
    {
        public bool Pressed { get; set; }

        public void OnButtonPressed()
        {
            Pressed = true;
            GameObject.Find("FogInfo").transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
