using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Scripts
{
    public class ThoraldMoveButton : MonoBehaviour
    {
        public bool Pressed { get; set; }

        public void OnButtonPressed()
        {
            Pressed = !Pressed;
            if(Pressed)
            {
                GetComponentInChildren<TMP_Text>().text = "End Move";
                FindObjectOfType<ThoraldController>().Show();
            }
            else
            {
                EndMove();
            }
        }

        public void EndMove()
        {
            GetComponentInChildren<TMP_Text>().text = "Thorald Move";
            FindObjectOfType<ThoraldController>().Hide();
            Pressed = false;
        }
    }
}

