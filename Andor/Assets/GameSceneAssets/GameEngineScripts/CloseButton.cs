using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class CloseButton : MonoBehaviour
    {
        public void OnButtonPressed()
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
}

