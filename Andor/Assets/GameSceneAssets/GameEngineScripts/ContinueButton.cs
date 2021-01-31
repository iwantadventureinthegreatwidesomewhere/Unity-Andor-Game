using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class ContinueButton : MonoBehaviour
    {
        public bool Pressed { get; set; }

        public void OnButtonPressed()
        {
            Pressed = true;
        }
    }
}
