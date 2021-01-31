using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    GameObject PriceInWillPowers;
    GameObject currencySelection;
    Button proposalButton;
    bool isDwarf= false;
    bool eventCard = false;   //One gold for 4 willpower

    public void Start()
    {
        Debug.Log("Hi");
        //Test use
        //
        //Get items clicked.
        //Text priceInWillPowers = GameObject.Find("PrinceInWillPowers/willPowersPrice/").GetComponent<Text>();
        GameObject currencySelection = GameObject.Find("CurrencyType");
        if (isDwarf)
        {
            storeForDwarf();
        }else {
            currencySelection.gameObject.SetActive(false);
        }
    }

    void storeForDwarf() 
    {
            //Set currency selection panel visible
            currencySelection.gameObject.SetActive(true);
            //Set price display in Willpower visible
            PriceInWillPowers.gameObject.SetActive(true);

    }


    //TODO: change price dynamically


    void goldPriceCalculator() {
    }

    void willPowerCalculator() {
        //Calculate total price in willPower
        
    }


    void DisplayPrice()
    {

        //willPowersPrice.text = "Price: " + priceIn;
    }


}