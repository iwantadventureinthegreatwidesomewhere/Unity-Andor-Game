using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSelection : MonoBehaviour
{
    public static HeroSelection HS;
    
    public GameObject Lobby;
    public int mySelectedCharacter;

    public GameObject[] allCharacters;

    private void OnEnable() {
        
        if(HeroSelection.HS == null) {
            HeroSelection.HS = this;
        }else{

            if(HeroSelection.HS != this){
                Destroy(HeroSelection.HS.gameObject);
                HeroSelection.HS = this;
            }

        }

        DontDestroyOnLoad(this.gameObject);
    }
    
    void Start(){
        if(PlayerPrefs.HasKey("MyCharacter")){
            mySelectedCharacter = PlayerPrefs.GetInt("MyCharacter");
        } else {
            mySelectedCharacter = 0;
            PlayerPrefs.SetInt("MyCharacter", mySelectedCharacter);
        }
    }

    public void OnClickCharacterPick(int whichCharacter){
        if(HeroSelection.HS != null){
            HeroSelection.HS.mySelectedCharacter = whichCharacter;
            PlayerPrefs.SetInt("MyCharacter", whichCharacter);
        }
        Lobby.SetActive(true);

    }
}