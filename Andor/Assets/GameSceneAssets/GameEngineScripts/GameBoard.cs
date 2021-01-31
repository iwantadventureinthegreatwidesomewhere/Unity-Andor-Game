using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//What is this class for we already have a game manager???
namespace Scripts
{
    public class GameBoard : MonoBehaviour
    {
        public static GameBoard instance;

        public GameObject sunriseBox;

        private void Awake()
        {
            instance = this;
        }
    }
}

