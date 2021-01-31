using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PriceManager : MonoBehaviour
{
    // Start is called before the first frame update
    InputField inputPrice;
    GameObject ErrorPanel;
    int price;

    Text text;

    void Awake()
    {
        //Set up the reference
        text = GetComponent<Text>();

        //Reset the price

        if (int.TryParse(inputPrice.text, out price)){
        }
        else {
            //TODO: Handle inproper input
        }
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Price: " + price;
    }
}
