using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class HerbController : MonoBehaviour
    {

        void OnMouseDown()
        {
			//Do something
			GameObject FogInfo = GameObject.Find("FogInfo").transform.GetChild(0).gameObject;
			GameObject DUI = null;
			for (int i = 0; i < FogInfo.transform.childCount - 1; i++)
			{
				if (FogInfo.transform.GetChild(i).transform.name == "Description")
				{
					DUI = FogInfo.transform.GetChild(i).gameObject;
				}
			}
			DUI.GetComponent<Text>().text = "Herb";
			FogInfo.SetActive(true);
		}
    }
}
