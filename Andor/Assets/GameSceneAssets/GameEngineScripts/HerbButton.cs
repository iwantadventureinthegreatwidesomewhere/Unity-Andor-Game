using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class HerbButton : MonoBehaviour
    {
        public bool Pressed { get; set; }
        public MedicinalHerb Herb { get; set; }

        public void OnButtonPressed()
        {
            Pressed = true;
            Herb.NotifyPressed();
        }

        public void SetActive(bool toggle)
        {
            gameObject.SetActive(toggle);
        }

        public void ToggleActive()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}

