using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class ThoraldController : MonoBehaviour
    {
        public GameObject currentPosition;
        HeroController currentPlayer;
        int spacesMoved;

        // Start is called before the first frame update
        void Start()
        {
            GameObject.Find("ActionOptions").GetComponentInChildren<ThoraldMoveButton>().gameObject.SetActive(true);
            //TODO: Test
            currentPlayer = FindObjectOfType<HeroController>();
        }

        public void Show()
        {
            if (currentPlayer.HasTimeLeft())
            {
                foreach (GameObject x in currentPosition.GetComponent<Node>().getNeighbours())
                {
                    x.GetComponent<MeshRenderer>().enabled = true;
                    x.GetComponent<BoxCollider>().enabled = true;
                }
            }
        }
        public void Hide()
        {
            foreach (GameObject x in currentPosition.GetComponent<Node>().getNeighbours())
            {
                x.GetComponent<MeshRenderer>().enabled = false;
                x.GetComponent<BoxCollider>().enabled = false;
            }
        }

        public void Move(GameObject newPos)
        {
            Hide();
            //Thorald moves 4 spaces per timestep
            if(spacesMoved % 4 == 0)
            {
                currentPlayer.TimeTrackTick();
            }
            currentPosition = newPos;
            //transform.position = currentPosition.transform.position;
            SetPosition();
            //If you've moved 4 spaces, don't move anymore
            if(spacesMoved % 4 == 3)
            {
                FindObjectOfType<ThoraldMoveButton>().EndMove();
            }
            else
            {
                Show();
            }
            spacesMoved++;
        }

        public void SetNode(GameObject pos)
        {
            currentPosition = pos;
            SetPosition();
        }

        public void SetPosition()
        {
            Vector3 pos = currentPosition.transform.position;
            pos.x -= 0.25f;
            pos.z += 0.25f;
            transform.position = pos;
        }

        //TODO: Call when player ends turn
        public void ResetSpacesMoved()
        {
            Hide();
            spacesMoved = 0;
        }
    }
}

